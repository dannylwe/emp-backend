using System.Globalization;
using System.Text;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using intEmp.data;
using intEmp.Dto;
using intEmp.Entity;
using intEmp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace intEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var employees = await _context.Employees.Include(e => e.Salary).ToListAsync();
            var employeeResponseDtos = _mapper.Map<List<EmployeeResponseDto>>(employees);
            return Ok(employeeResponseDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetSingleEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound("");
            }
            var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(employee);
            return Ok(employeeResponseDto);
        }

        [AllowAnonymous]
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

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateHero(int id, UpdateEmployeeDto updateEmployee)
        {   
            var dbEmployee = await _context.Employees.FindAsync(id);
            if(dbEmployee is null)
            {
                return NotFound("");
            }
            dbEmployee.Email = updateEmployee.Email;
            dbEmployee.FirstName = updateEmployee.FirstName;
            dbEmployee.LastName = updateEmployee.LastName;
            dbEmployee.Phone = updateEmployee.Phone;
            dbEmployee.Department = updateEmployee.Department;
            await _context.SaveChangesAsync();

            var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(dbEmployee);
            return Ok(employeeResponseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv()
        {
            var employees = await _context.Employees.ToListAsync();

            var records = employees.Select(e => new EmployeeCsvDto
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Department = e.Department
            }).ToList();

            byte[] csvData;
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
                    csv.WriteRecords(records);
                }
                csvData = memoryStream.ToArray();
            }

            // Return CSV file as a downloadable file
            return File(csvData, "text/csv", "employees.csv");
        }
    }
}
