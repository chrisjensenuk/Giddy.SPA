using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Filters
{
    public class AuthorizeOperationAttribute : Attribute
    {
        public string Operation { get; set; }
        public AuthorizeOperationAttribute(string operation)
        {
            Operation = operation;
        }
    }
}