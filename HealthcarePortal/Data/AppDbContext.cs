using HealthcarePortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace HealthcarePortal.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "Nagendra",
                    Email = "nagendra@email.com",
                    Phone = "9876543210",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Department = "Cardiology",
                    RegisteredOn = new DateTime(2026, 1, 1), // ← fixed date!
                    IsActive = true
                },
                new Patient
                {
                    Id = 2,
                    Name = "Priya",
                    Email = "priya@email.com",
                    Phone = "9876543211",
                    DateOfBirth = new DateTime(1990, 8, 20),
                    Department = "Neurology",
                    RegisteredOn = new DateTime(2026, 1, 1), // ← fixed date!
                    IsActive = true
                }
            );
        
        }
        public DbSet<Patient> Patients { get; set; }
        

    }
}
