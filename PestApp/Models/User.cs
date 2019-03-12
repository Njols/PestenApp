using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class User
    {
        [Display(Name = "E-mail Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "E-mails don't match")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password needs to be between 5 and 100 characters in length")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
