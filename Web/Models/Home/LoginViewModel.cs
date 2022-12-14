using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Home
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string UserPassword { get; set; }
    }
}

