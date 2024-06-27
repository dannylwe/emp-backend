using intEmp.data;
using intEmp.Dto;
using intEmp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginEmployeeDto loginDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == loginDto.Email);
            if (employee == null || !AuthService.VerifyPassword(loginDto.Password, employee.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = AuthService.GenerateToken(employee);
            return Ok(new { Token = token });
        }
    }
    
}
