using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Giddy.SPA.Hosting.Models
{
    //A wrapper for Errors and ModelStateErrors so I can be more explicit about the representation of the error in JSON
    public class GiddyHttpError
    {
        public string Message { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }

        public GiddyHttpError()
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
        }

        public GiddyHttpError(string message = null) : base()
        {
            Message = message ?? "Your request is invalid.";
        }

        public GiddyHttpError(ModelStateDictionary modelState, string message = null) : this (message)
        {
            
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