using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace Giddy.SPA.Hosting.ExtensionMethods
{
    public static class ValidationResultExtensionMethods
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix)
        {
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    string key = string.IsNullOrEmpty(prefix) ? error.PropertyName : prefix + "." + error.PropertyName;
                    modelState.AddModelError(key, error.ErrorMessage);
                    //To work around an issue with MVC: SetModelValue must be called if AddModelError is called.
                    modelState.SetModelValue(key, new ValueProviderResult(error.AttemptedValue ?? "", (error.AttemptedValue ?? "").ToString(), CultureInfo.CurrentCulture));
                }
            }
        }

    }
}