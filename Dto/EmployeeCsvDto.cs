namespace intEmp.Dto
{
    public class EmployeeCsvDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
    }
}