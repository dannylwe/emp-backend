using intEmp.Dto;

namespace intEmp.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetAllEmployees();
        Task<EmployeeResponseDto?> GetSingleEmployee(int id);
        Task<EmployeeResponseDto?> CreateEmployee(CreateEmployeeDto employeeDto);
        Task<EmployeeResponseDto?> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto);
        Task<bool> DeleteEmployee(int id);
        Task<byte[]> ExportCsv();
    }
}