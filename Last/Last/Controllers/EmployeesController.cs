using Last.Data;
using Last.Models;
using Last.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Last.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    DepartmentId = e.DepartmentId ?? 0,
                    DepartmentName = e.Department != null ? e.Department.DeptName : null
                })
                .ToList();

            return Ok(allEmployees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var empViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId ?? 0,
                DepartmentName = employee.Department != null ? employee.Department.DeptName : null
            };

            return Ok(empViewModel);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeesViewModel employee)
        {
            var dept = dbContext.Departments.Find(employee.DepartmentId);
            if (dept == null)
            {
                return BadRequest();
            }

            var employeeEntity = new Employee
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeViewModel employeeViewModel)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }

            emp.Name = employeeViewModel.Name;
            emp.Email = employeeViewModel.Email;
            emp.Phone = employeeViewModel.Phone;
            emp.Salary = employeeViewModel.Salary;
            emp.DepartmentId = employeeViewModel.DepartmentId;

            dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var emp = dbContext.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
