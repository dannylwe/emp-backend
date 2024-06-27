using System.ComponentModel.DataAnnotations;

namespace intEmp.Entity
{
    public class Employee
    {   [Key]
        public int Id {get; set;}
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email {get; set;} = string.Empty;
        public string Phone {get; set;} = string.Empty;
        public string Department {get; set;} = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Salary? Salary { get; set; }
    }
}
