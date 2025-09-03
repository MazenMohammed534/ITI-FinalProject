namespace Last.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; } // <-- Add this line!
        public string DepartmentName { get; set; }
    }
}
