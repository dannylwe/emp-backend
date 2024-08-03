using intEmp.Dto;
using intEmp.Entity;
using intEmp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace intEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetSingleEmployee(int id)
        {
            var employee = await _employeeService.GetSingleEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }   

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto Employee)
        {   
            var employee = await _employeeService.CreateEmployee(Employee);
            if (employee == null)
            {
                return Conflict("Email already exists.");
            }
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeService.UpdateEmployee(id, updateEmployeeDto);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.DeleteEmployee(id);
            if (!employee) {
                return NotFound();
            }
            return NoContent();
        }

        // [HttpDelete("all")]
        // public async Task<IActionResult> DeleteAllEmployees()
        // {   
        //     var allEmployees = await _context.Employees.Include(e => e.Salary).ToListAsync();

        //     foreach (Employee employee in allEmployees) {
        //         _context.Employees.Remove(employee);
        //     }
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv()
        {
            var csvData = await _employeeService.ExportCsv();
            return File(csvData, "text/csv", "employees.csv");
        }
    }
}
