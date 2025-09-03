using System.ComponentModel.DataAnnotations;

namespace Last.ViewModels
{
    public class AddEmployeesViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
