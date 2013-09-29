using AttributeRouting.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Giddy.SPA.Hosting.Controllers.Http
{
    /// <summary>
    /// REST HTTP API.
    /// Embracing HTTP. This is not WCF so I want to commuicate at the HTTP level and not let ASP.NET abstract the transport away from me.
    /// E.g. I'll never thow an exception. I'll always manually return a HTTP 500 response message
    /// I'll always accept the originating HTTPRequestMessage 
    /// I'll always return a HttpResponseMessage. Not relying on framwokr abstractions
    /// </summary>
    public class HomeAPIController : ApiControllerBase
    {
        [HttpPost]
        [Authorize]
        [POST("api/addfoo")]
        public HttpResponseMessage AddFoo(HttpRequestMessage request, [FromBody]string text)
        {
            return BuildHttpResponse(request, () =>
            {
                return request.CreateResponse<string>(HttpStatusCode.OK, text + "Foo");
            });
        }
    }
}
