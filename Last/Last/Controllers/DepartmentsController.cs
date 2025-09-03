using Last.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Last.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = dbContext.Departments
                .Select(d => new { d.Id, d.DeptName })
                .ToList();
            return Ok(departments);
        }
    }
}