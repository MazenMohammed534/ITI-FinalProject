using System.ComponentModel.DataAnnotations;

namespace Last.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public decimal Salary { get; set; }

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }

    }
}
