using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Giddy.SPA.Hosting.Models
{
    
    public class ModelStateError
    {
        public string Message { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }

        public ModelStateError(ModelStateDictionary modelState, string message = null)
        {
            Message = message ?? "Your request is invalid.";
            Errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var item in modelState)
            {
                var itemErrors = new List<string>();
                foreach (var childItem in item.Value.Errors)
                {
                    itemErrors.Add(childItem.ErrorMessage);
                }
                Errors.Add(item.Key, itemErrors);
            }
        }

        
    }
}