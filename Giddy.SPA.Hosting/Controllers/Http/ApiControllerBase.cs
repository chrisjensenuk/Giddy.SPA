using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Giddy.SPA.Hosting.Controllers.Http
{
    public abstract class ApiControllerBase : ApiController
    {

        protected HttpResponseMessage BuildHttpResponse(HttpRequestMessage request, Action innerCode)
        {
            try
            {
                innerCode.Invoke();

                return request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return HandleException(request, ex);
            }
        }

        protected HttpResponseMessage BuildHttpResponse<T>(HttpRequestMessage request, Func<T> innerCode)
        {
            HttpResponseMessage response;

            try
            {
                T invoked = innerCode.Invoke();

                if (typeof(T) == typeof(HttpResponseMessage))
                {
                    response = invoked as HttpResponseMessage;
                }
                else
                {
                    response = request.CreateResponse<T>(HttpStatusCode.OK, invoked);
                }
            }
            catch (Exception ex)
            {
                response = HandleException(request, ex);
            }

            return response;
        }

        private HttpResponseMessage HandleException(HttpRequestMessage request, Exception ex)
        {
            return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
