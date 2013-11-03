using AttributeRouting.Web.Mvc;
using Giddy.SPA.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Giddy.SPA.Hosting.Controllers.Http
{
    public class RouteApiController : ApiControllerBase
    {

        private readonly IDurandalRouteManager _durandalRouteManager; 

        public RouteApiController(IDurandalRouteManager durandalRouteMgr)
        {
            _durandalRouteManager = durandalRouteMgr;
        }

        [HttpGet]
        [AllowAnonymous]
        [GET("api/userroutes")]
        public HttpResponseMessage UserRoutes(HttpRequestMessage request)
        {
            return BuildHttpResponse(request, () =>
            {
                return _durandalRouteManager.GetRoutes();
            });
        }
    }
}
