﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Olx.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }  
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [System.Web.Mvc.Remote("AlreadyExits","Account",ErrorMessage="Account with this email already exists")]
        public string Email { get; set; }     

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Mobile number must be 10 digit")]
        public string Mobile { get; set; }
    }
}
