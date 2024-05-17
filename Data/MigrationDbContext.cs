using Microsoft.EntityFrameworkCore;
using FisioSolution.Models;
using Microsoft.Extensions.Logging;

namespace FisioSolution.Data
{
    public class MigrationDbContext : DbContext
    {
        public MigrationDbContext(DbContextOptions<MigrationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Physio>().HasData(
                    new Physio { PhysioId = 1, Name = "Daniel", RegistrationNumber = 1783, Password = "1234", Availeable = true, OpeningTime = TimeSpan.FromHours(8), ClosingTime = TimeSpan.FromHours(18), Price = 50.00m },
                    new Physio { PhysioId = 2, Name = "Maria", RegistrationNumber = 1700, Password = "futbol3", Availeable = false, OpeningTime = TimeSpan.FromHours(9), ClosingTime = TimeSpan.FromHours(17), Price = 60.00m },
                    new Physio { PhysioId = 3, Name = "Jaime", RegistrationNumber = 1600, Password = "cocacola27", Availeable = true, OpeningTime = TimeSpan.FromHours(9), ClosingTime = TimeSpan.FromHours(17), Price = 60.00m }
                );

                modelBuilder.Entity<Patient>().HasData(
                    new Patient { PatientId = 1, Name = "John Doe", Dni = "730151515", Password = "1234", BirthDate = new DateTime(1980, 5, 10), Weight = 80.5m, Height = 180.0m, Insurance = true },
                    new Patient { PatientId = 2, Name = "Pedro Martínez", Dni = "730203040", Password = "pass123", BirthDate = new DateTime(1993, 5, 14), Weight = 70.5m, Height = 172.0m, Insurance = true }
                );

                modelBuilder.Entity<Treatment>().HasData(
                    new Treatment { TreatmentId = 1, PhysioId = 1, PatientId = 1, TreatmentCause = "Dolor de espalda", TreatmentDate = DateTime.Now, MoreSessionsNeeded = true }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physio> Physios { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
    }
}

