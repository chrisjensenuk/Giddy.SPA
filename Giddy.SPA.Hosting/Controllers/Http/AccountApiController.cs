using AttributeRouting.Web.Http;
using Giddy.SPA.Hosting.Filters.Http;
using Giddy.SPA.Hosting.Models;
using Giddy.SPA.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Giddy.SPA.Hosting.Controllers.Http
{
    //[Authorize]
    public class AccountApiController : ApiControllerBase
    {
        private readonly ISecurityManager _securityMgr;

        public AccountApiController(ISecurityManager securityMgr)
        {
            _securityMgr = securityMgr;
        }

        

        [HttpGet]
        [AllowAnonymous]
        [GET("/api/checkloggedin")]
        public HttpResponseMessage CheckLoggedIn(HttpRequestMessage request)
        {
            return BuildHttpResponse(request, () =>
            {
                return _securityMgr.LoggedIn;
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [POST("/api/login")]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginModel model)
        {
            return BuildHttpResponse(request, () =>
            {
                var loggedIn = _securityMgr.Login(model);

                if (!loggedIn)
                {
                    return request.CreateGiddyErrorResponse("The user name or password provided is incorrect.");
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
                    _securityMgr.Logout();
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

                    var antiforgeryCookie = new CookieHeaderValue(AntiForgeryConfig.CookieName, newCookieToken);
                    
                    response.Headers.AddCookies(new[] { antiforgeryCookie });
                    return response;
                });
        }

        [HttpPost]
        [AllowAnonymous]
        [POST("/api/register")]
        [ValidateAntiForgeryToken]
        public virtual HttpResponseMessage Register(HttpRequestMessage request, RegisterModel model)
        {
            return BuildHttpResponse(request, () =>
            {
                // Attempt to register the user
                try
                {
                    _securityMgr.Register(model);

                    return request.CreateResponse(HttpStatusCode.OK, true);
                }
                catch (SecurityManagerException ex)
                {
                    return request.CreateGiddyErrorResponse(ex.Message);
                }

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [POST("api/manage")]
        public virtual HttpResponseMessage Manage(HttpRequestMessage request, LocalPasswordModel model)
        {
            return BuildHttpResponse(request, () =>
            {
                try
                {
                    _securityMgr.ChangePassword(model);
                    return request.CreateResponse(HttpStatusCode.OK, true);
                }
                catch (SecurityManagerException ex)
                {
                    return request.CreateGiddyErrorResponse(ex.Message);
                }
            });
        }
    }
}

