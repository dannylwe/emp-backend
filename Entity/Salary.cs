using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace intEmp.Entity
{
    public class Salary
    {   [Key]
        public int Id {get; set;}
        [ForeignKey("Employee")]
        public int EmployeeId {get; set;}
        public int BaseSalary {get; set;}
        public int Bonus {get; set;}
        public string Date {get; set;} = string.Empty;
        public required Employee Employee { get; set; }
    }
}
