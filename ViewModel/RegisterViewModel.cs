using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "pragimtech.com", ErrorMessage = "Email domain must be pragimtech.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] //Note: To mask the entered password we use this attribute.
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
