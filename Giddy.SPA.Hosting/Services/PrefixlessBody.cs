using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Giddy.SPA.Hosting.Services
{
    //http://stackoverflow.com/questions/17106151/how-to-remove-prefixes-from-modelstate-keys
    public class PrefixlessBodyModelValidator : IBodyModelValidator
    {
        // When ModelState is converted to JSON then an auwanted prefix is added to the property name. So using this instead
        
        private readonly IBodyModelValidator _innerValidator;

        public PrefixlessBodyModelValidator(IBodyModelValidator innerValidator)
        {
            if (innerValidator == null)
            {
                throw new ArgumentNullException("innerValidator");
            }

            _innerValidator = innerValidator;
        }

        public bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix)
        {
            // Remove the keyPrefix but otherwise let innerValidator do what it normally does.
            return _innerValidator.Validate(model, type, metadataProvider, actionContext, String.Empty);
        }
    }

}