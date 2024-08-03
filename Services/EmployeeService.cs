using System.Globalization;
using System.Text;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using intEmp.data;
using intEmp.Dto;
using intEmp.Entity;
using Microsoft.EntityFrameworkCore;

namespace intEmp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<EmployeeResponseDto?> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            if (await _context.Employees.AnyAsync(e => e.Email == employeeDto.Email))
            {
                return null;
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            employee.PasswordHash = AuthService.HashPassword(employeeDto.Password);

            employee.Salary = new Salary
            {
                BaseSalary = employeeDto.BaseSalary,
                Bonus = 0,
                Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Employee = employee
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> ExportCsv()
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

            return csvData;
        }

        public async Task<List<EmployeeResponseDto>> GetAllEmployees()
        {
            var employees = await _context.Employees.Include(e => e.Salary).ToListAsync();
            return  _mapper.Map<List<EmployeeResponseDto>>(employees); 
        }

       
        public async Task<EmployeeResponseDto?> GetSingleEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Salary)
                .FirstOrDefaultAsync(e => e.Id == id);
            return employee == null ? null : _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<EmployeeResponseDto?> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _context.Employees.Include(e => e.Salary).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return null;
            }

            employee.Email = updateEmployeeDto.Email;
            employee.FirstName = updateEmployeeDto.FirstName;
            employee.LastName = updateEmployeeDto.LastName;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Department = updateEmployeeDto.Department;
            employee.PasswordHash = AuthService.HashPassword(updateEmployeeDto.Password);

            if (employee.Salary != null)
            {
                employee.Salary = new Salary
                {
                    BaseSalary = updateEmployeeDto.BaseSalary,
                    Bonus = updateEmployeeDto.Bonus,
                    Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Employee = employee
                };
            }
            else
            {
                employee.Salary.BaseSalary = updateEmployeeDto.BaseSalary;
                employee.Salary.Bonus = updateEmployeeDto.Bonus;
                employee.Salary.Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeResponseDto>(employee);

        }
    }
}