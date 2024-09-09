using EmployeeManagement.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeCreateViewModel
    {
        [Required] //Note: This Required attribute will make sure the input forms field on the web page is not null.
        [MaxLength(50, ErrorMessage = "Name can not exceed 50 chars")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
