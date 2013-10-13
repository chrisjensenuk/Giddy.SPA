using Giddy.SPA.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Giddy.SPA.Hosting.Services
{

    public interface ISecurityManager
    {
        bool LoggedIn {get;}
        bool Login(LoginModel login);
        void Logout();
        void Register(RegisterModel registerModel);
    }

    public class SecurityManagerException : Exception
    {
        public SecurityManagerException(string message) : base(message) {}
        public SecurityManagerException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class WebSecurityManager : ISecurityManager
    {
        public bool LoggedIn
        {
            get
            {
                return (HttpContext.Current != null)? HttpContext.Current.User.Identity.IsAuthenticated : false;
            }
        }


        public bool Login(LoginModel login)
        {
            return WebSecurity.Login(login.UserName, login.Password, persistCookie: login.RememberMe);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public void Register(RegisterModel registerModel)
        {
            try
            {
                WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password);
                WebSecurity.Login(registerModel.UserName, registerModel.Password);
            }
            catch (MembershipCreateUserException e)
            {
                throw new SecurityManagerException(ErrorCodeToString(e.StatusCode), e);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}