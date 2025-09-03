namespace Last.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
