using intEmp.data;
using intEmp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace intEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetSingleEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound("");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateHero(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateHero(Employee updateEmployee)
        {
            var dbEmployee = await _context.Employees.FindAsync(updateEmployee.Id);
            if(dbEmployee is null)
            {
                return NotFound("");
            }
            dbEmployee.Email = updateEmployee.Email;
            dbEmployee.FirstName = updateEmployee.FirstName;
            dbEmployee.LastName = updateEmployee.LastName;
            dbEmployee.Phone = updateEmployee.Phone;
            await _context.SaveChangesAsync();
            return Ok(dbEmployee);
        }
    }
}
