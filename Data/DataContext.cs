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

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Employee && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var employee = (Employee)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    employee.CreatedAt = DateTime.UtcNow;
                }
                employee.UpdatedAt = DateTime.UtcNow;
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }
    }
    
}