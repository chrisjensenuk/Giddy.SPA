using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Models
{
    public class LoginModel
    {
        //[Required]
        [MinLength(5)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(l => l.UserName).Length(5, 100).EmailAddress();
            RuleFor(l => l.Password).Length(5, 100);
        }
    }
}