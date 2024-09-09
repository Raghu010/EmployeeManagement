using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>(); Roles = new List<string>(); //Note: We are initializing them so that they don't throw null reference exceptions.
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}
