using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using Giddy.SPA.Hosting.Services;
using FluentValidation.Results;
using System.Web.Http.ModelBinding;
using Giddy.SPA.Hosting.ExtensionMethods;
using FluentValidation;
using Giddy.SPA.Hosting.Models;

namespace Giddy.SPA.Hosting.Filters.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ValidatorFactory _validatorFactory;
        public ValidationActionFilterAttribute(ValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            //check fluent validation errors
            foreach (KeyValuePair<string, object> arg in actionContext.ActionArguments.Where(a => a.Value != null))
            {
                var argType = arg.Value.GetType();
                Type generic = typeof(IValidator<>);
                Type specific = generic.MakeGenericType(argType);

                var validator = _validatorFactory.GetValidator(specific);
                if (validator != null)
                {
                    var validationResult = validator.Validate(arg.Value);
                    if (!validationResult.IsValid)
                    {
                        validationResult.AddToModelState(modelState, "");
                    }
                }
            }

            if (!modelState.IsValid)
            {
                //wrap the modelState in object I can control the json representation of.
                var httpError = new GiddyHttpError(modelState);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, httpError);
            }
        }
    }
}