namespace intEmp.Dto
{
    public class CreateEmployeeDto
    {
        public required string FirstName {get; set;}
        public required string  LastName {get; set;}
        public required string  Email {get; set;}
        public string? Phone {get; set;}
        public string?  Department {get; set;}
        public required string Password { get; set; }
        public int BaseSalary { get; set; } = 0;
    }

    public class UpdateEmployeeDto
    {
        public required string FirstName {get; set;}
        public required string  LastName {get; set;}
        public required string  Email {get; set;}
        public required string Phone {get; set;}
        public required string  Department {get; set;}
        public required string Password { get; set; }
        public int BaseSalary { get; set; } = 0;
        public int Bonus { get; set; } = 0;
    }

    public class EmployeeResponseDto
    {   
        public required int Id { get; set; }
        public required string FirstName {get; set;}
        public required string  LastName {get; set;}
        public required string  Email {get; set;}
        public string? Phone {get; set;}
        public required string  Department {get; set;}
        public SalaryResponseDto? Salary { get; set; }
    }

    public class SalaryResponseDto
    {
        public int BaseSalary { get; set; }
        public int Bonus { get; set; }
        public string? Date { get; set; }
    }
}
