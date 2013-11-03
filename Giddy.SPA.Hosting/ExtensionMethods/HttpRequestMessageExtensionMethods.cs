using Giddy.SPA.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Giddy.SPA.Hosting
{
    public static class HttpRequestMessageExtensionMethods
    {
        public static HttpResponseMessage CreateGiddyErrorResponse(this HttpRequestMessage request, string message)
        {
            var httpError = new GiddyHttpError(message);
            return request.CreateResponse(HttpStatusCode.BadRequest, message);
        }
    }
}