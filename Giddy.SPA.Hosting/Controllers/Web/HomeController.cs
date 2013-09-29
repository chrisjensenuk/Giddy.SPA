using AttributeRouting.Web.Mvc;
using Giddy.SPA.Hosting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Giddy.SPA.Hosting.Controllers.Web
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        [GET("/")]
        public virtual ActionResult Index()
        {
            return View();
        }

        
        private void Foo()
        {

        }
        
        private DateTime Foo2(string parameter1, string parameter2)
        {
            return WithLogging(new { parameter1 = parameter1, parameter2 = parameter2 }, () =>
            {
                return WithSecurity("operationMask{0}".F(parameter1), () =>
                {
                    return new DateTime();
                });
            });
        }


        private DateTime Foo3(string parameter1, string parameter2)
        {
            return WithLogging(new { parameter1 = parameter1, parameter2 = parameter2 }, () =>
            {
                //check access
                if(TryAuth("operationMask{0}", parameter1))
                {
                    //do something if authroized
                }

                //This with throw a security exception if don't have access
                Auth("operationMask{0}", parameter2);

                return new DateTime();
            });
        }

        private bool TryAuth(string operationMask, params string[] args)
        {
            var operation = string.Format(operationMask, args);

            return true;
        }

        private void Auth(string operationMask, params string[] args)
        {
            var operation = string.Format(operationMask, args);


            //user is not allowed to perfrom this operation
            var ex = new UnauthorizedAccessException("The User {0} has been refused access to operation {1}".F(User.Identity.Name, operation));
            throw ex;
        }

        private T WithSecurity<T>(string operation, Func<T> codeToInvoke)
        {
            //check whether the user is allowed to execute the operation
            if(!CheckSecurity(operation, User.Identity.Name))
            {
                //user is not allowed to perfrom this operation
                var ex = new UnauthorizedAccessException("The User {0} has been refused access to operation {1}".F(User.Identity.Name, operation));
                throw ex;
            }

            return codeToInvoke.Invoke();
        }

        private bool CheckSecurity(string userId, string operation)
        {
            return true;
        }

        private void WithLogging2(Action codeToInvoke)
        {
            //TODO start logging here
            codeToInvoke.Invoke();
            //TODO end logging here
        }

        private T WithLogging<T>(object valuesToLog, Func<T> codeToInvoke, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int CallerLineNumber = 0)
        {
            var userId = User.Identity.Name;
            var vals = LoggingValuesToString(valuesToLog);
            var message = string.Format("MemberName:{0}; Params:{1}", callerMemberName, vals);

            //TODO start logging here
            return codeToInvoke.Invoke();
            //TODO end logging here
        }

        private string LoggingValuesToString(object valuesToLog)
        {
            return string.Join(", ", valuesToLog.GetType().GetProperties()
                .Select(p => string.Format("{0}:\"{1}\"", p.Name, p.GetValue(valuesToLog, null).ToNullString()))
                .ToArray()
                );
        }

        private void WriteLog(string message)
        {

        }

    }

    public static class StringExtensionMethods
    {
        public static string F(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }

    public static class ObjectExtensionMethods
    {
        public static string ToNullString(this object obj)
        {
            return (obj == null) ? "" : obj.ToString();
        }
    }
    
}
