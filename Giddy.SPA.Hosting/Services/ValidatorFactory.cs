using FluentValidation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Services
{
    /// <summary>
    /// A factory to return validators.
    /// </summary>
    public class ValidatorFactory : IValidatorFactory
    {
        protected IServiceProvider _serviceprovider;
        public ValidatorFactory(Container serviceprovider)
        {
            _serviceprovider = serviceprovider;
        }

        public IValidator<T> GetValidator<T>()
        {
            return _serviceprovider.GetService(typeof(IValidator<T>)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            throw new NotImplementedException();
        }
    }
}