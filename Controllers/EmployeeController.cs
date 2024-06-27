using AutoMapper;
using intEmp.data;
using intEmp.Dto;
using intEmp.Entity;
using intEmp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace intEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<ActionResult<Employee>> CreateHero(CreateEmployeeDto employee)
        {   
            if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
            {
                return Conflict("Email already exists.");
            }

            var Employee = _mapper.Map<Employee>(employee);
            Employee.PasswordHash = AuthService.HashPassword(employee.Password);
            
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            var employeeResponse = _mapper.Map<EmployeeResponseDto>(Employee);
            return Ok(employeeResponse);
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
