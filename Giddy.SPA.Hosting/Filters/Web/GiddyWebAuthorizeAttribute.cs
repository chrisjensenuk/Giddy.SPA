using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Giddy.SPA.Hosting.Security;
using Giddy.SPA.Hosting.Filters;
using Elmah.Mvc;

namespace Giddy.SPA.Hosting.Filter.Web
{
    /// <summary>
    /// A globalFilter to handle Authorization and Operation based security.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GiddyWebAuthorizeAttribute : AuthorizeAttribute, IActionFilter
    {
        
        public IAuthorizationManager AuthorizationMgr { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //if operation based security has been specified then authorize on the operation
            var operation = GetOperation(filterContext);
            if (operation != null)
            {
                AuthorizeByOperation(filterContext, operation);
                return;
            }

            //check if Elmah Controller. If so ensure the user has ViewElmah operation permission
            if (filterContext.Controller is ElmahController)
            {
                AuthorizeByOperation(filterContext, "ViewErrorLog2");
                return;
            }
        }

        private string GetOperation(ActionExecutingContext filterContext)
        {
            var operationAttribute = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeOperationAttribute), false).SingleOrDefault() as AuthorizeOperationAttribute;

            return (operationAttribute != null) ? operationAttribute.Operation : null;
        }

        private void AuthorizeByOperation(ActionExecutingContext filterContext, string operation)
        {

            //We need to check for parameter authorization at the Action point not the Authorize point so we can use the actual parameters passed into the method
            bool authorised = false;

            //get the placeholders from the operation - rather than looping through action parameters to match it is safer if I let the operation specifiy what paramters I'm looking for
            var regex = new Regex("{(.*?)}");
            var operationParams = regex.Matches(operation);

            foreach (var operationParam in operationParams)
            {
                operation = ReplaceOperationPlaceholder(operation, operationParam.ToString(), filterContext.ActionParameters);
            }

            authorised = AuthorizationMgr.CheckAccess(operation);

            if (!authorised)
            {
                //setting the reuslt will short curcuit the action
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }


        private string ReplaceOperationPlaceholder(string operation, string parameter, IDictionary<string, object> actionParameters)
        {
            //seekedParameter could be {companyId} or a child of a passed in model E.g. {model.CompanyId} or {model.Company.CompanyId}

            //Ideally I would like to pass a param list of generic funcs into the attribtue constructor but can't do this with .net at this time
            //e.g. [OperationAuthorize("the {0} {1} operation", new Func<ModelType>(m => m.CompanyId), new Func<ModelType>(m => m.AnotherParam))]
            //So I will just use reflection. Losts of Reflection in MVC anyway so whats a bit more? :)

            var seekedActionParam = parameter.ToString().Trim(new[] { '{', '}' });

            //using a func to recurse
            Func<string, object, string> findValue = null;
            findValue = (s, o) =>
            {
                var index = s.IndexOf('.');
                var segment = (index > -1) ? s.Substring(0, index) : s;

                //if o is null then we are at the root of the model
                var foundObj = (o == null) ? actionParameters[segment] : o.GetType().GetProperty(segment).GetValue(o, null);

                if (foundObj == null) throw new ArgumentException("cannot locate segment in model " + segment);

                if (index == -1)
                {
                    //we are at the end of our match so current object must hold the required value
                    return foundObj.ToString();
                }
                else
                {
                    //we're not there yet so go dwn the tree
                    return findValue(s.Substring(index + 1), foundObj);
                }
            };

            var result = findValue(seekedActionParam, null);

            operation = operation.Replace(parameter, result);

            return operation;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) { }
    }
}