using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient _client;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:7242/api/");
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _client.GetFromJsonAsync<List<EmployeeViewModel>>("employees");
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _client.GetFromJsonAsync<List<DepartmentViewModel>>("departments");
            ViewBag.Departments = new SelectList(departments, "Id", "DeptName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _client.PostAsJsonAsync("employees", model);
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error creating employee");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _client.GetFromJsonAsync<EmployeeViewModel>($"employees/{id}");
            if (employee == null) return NotFound();

            var departments = await _client.GetFromJsonAsync<List<DepartmentViewModel>>("departments");
            ViewBag.Departments = new SelectList(departments, "Id", "DeptName", employee.DepartmentId);

            var model = new UpdateEmployeeViewModel
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _client.GetFromJsonAsync<List<DepartmentViewModel>>("departments");
                ViewBag.Departments = new SelectList(departments, "Id", "DeptName", model.DepartmentId);
                return View(model);
            }

            try
            {
                var response = await _client.PutAsJsonAsync($"employees/{id}", model);
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                var errorMsg = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error updating employee: {errorMsg}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception: {ex.Message}");
            }

            var depts = await _client.GetFromJsonAsync<List<DepartmentViewModel>>("departments");
            ViewBag.Departments = new SelectList(depts, "Id", "DeptName", model.DepartmentId);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"employees/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}