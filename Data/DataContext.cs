using Microsoft.EntityFrameworkCore;
using intEmp.Entity;

namespace intEmp.data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        public DbSet<Employee> Employees {get; set;}
        public DbSet<Salary> Salaries {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithOne(e => e.Salary)
                .HasForeignKey<Salary>(s => s.EmployeeId);
        }
    }
    
}