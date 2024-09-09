namespace EmployeeManagement.ViewModel
{
    public class EmployeeEditViewModel : EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string ExsistingPhotoPath { get; set; }
    }
}
