using CoreApp.Model.Administration;
using CoreApp.Model.Attendance;
using CoreApp.Model.Employee;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<WorkExperience> WorkExperiences { get; set; } = null!;
        public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
        public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;        
        public DbSet<Holiday> Holidays { get; set; } = null!;
        public DbSet<AssessmentIndicator> AssessmentIndicators { get; set; } = null!;
        public DbSet<EmployeeAssessment> EmployeeAssessments { get; set; } = null!;
    }
}
