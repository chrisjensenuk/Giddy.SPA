using AttributeRouting.Web.Http;
using Giddy.SPA.Hosting.Filters.Http;
using Giddy.SPA.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;
using WebMatrix.WebData;

namespace Giddy.SPA.Hosting.Controllers.Http
{
    public class AccountApiController : ApiControllerBase
    {
        [HttpGet]
        [GET("/api/checkloggedin")]
        public HttpResponseMessage CheckLoggedIn(HttpRequestMessage request)
        {
            return BuildHttpResponse(request, () =>
            {
                return User.Identity.IsAuthenticated;
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [POST("/api/login")]
        [ValidateAntiForgeryToken]
        [ValidationActionFilter]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginModel model)
        {
            return BuildHttpResponse(request, () =>
            {
                var loggedIn = WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe);

                if (!loggedIn)
                {
                    return request.CreateResponse(HttpStatusCode.Unauthorized, "The user name or password provided is incorrect.");
                }

                return request.CreateResponse(HttpStatusCode.OK, true);
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [POST("/api/logout")]
        public HttpResponseMessage LogOut(HttpRequestMessage request)
        {
            return BuildHttpResponse(request, () =>
                {
                    WebSecurity.Logout();
                });
        }

        /// <summary>
        /// In the real world I would probably do a full form post back and write out the @AntiForgery.GetHtml() into the HTML body. This is
        /// so I can redirect to a HTTPS session if we landed from a HTTP request.  However as this is a demo app I want to see if I can get
        /// AntiForgery working over AJAX.  This function must be called after login or log out as the token value changes depending if you 
        /// are authenticated or not.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [GET("/api/rvt")]
        public HttpResponseMessage Rvt(HttpRequestMessage request)
        {
            //Once we've logged in we need to get a Request Validation Token as a seperate get so we can get access to the cookies
            return BuildHttpResponse(request, () =>
                {
                    string newCookieToken, formToken;
                    AntiForgery.GetTokens(null, out newCookieToken, out formToken);

                    var response = request.CreateResponse(HttpStatusCode.OK, formToken);
                    response.Headers.AddCookies(new[] { new CookieHeaderValue(AntiForgeryConfig.CookieName, newCookieToken) });
                    return response;
                });
        }
    }
}
