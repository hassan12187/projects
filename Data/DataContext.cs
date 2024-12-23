using Hr_Vacancy_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Hr_Vacancy_Managment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Applicant_Vacancy> Applicant_Vacancy { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasOne(e => e.department).WithMany(d => d.employees).HasForeignKey(s => s.Depart_ID);
        }
    }
}
