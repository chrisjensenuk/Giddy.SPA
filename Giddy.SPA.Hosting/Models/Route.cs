﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Models
{
    /// <summary>
    /// A durandal route
    /// </summary>
    public class DurandalRoute
    {
        /*
        router.map([
            { route: '', title:'Home', moduleId: 'homeScreen', nav: true },
            { route: 'customer/:id', moduleId: 'customerDetails'}
        ]);
         */

        public string[] Route { get; set; }
        public string Title { get; set; }
        public string ModuleId { get; set; }
        public bool? Nav {get;set;}
    }
}