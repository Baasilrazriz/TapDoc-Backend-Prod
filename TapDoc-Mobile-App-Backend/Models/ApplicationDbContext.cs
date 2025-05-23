using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("MyDBContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<DoctorCategories> DoctorCategories { get; set; }
        public DbSet<DoctorDetails> DoctorDetails { get; set; }
        public DbSet<DoctorRatings> DoctorRatings { get; set; }
        public DbSet<EmergencyContacts> EmergencyContacts { get; set; }
        public DbSet<EmergencyServices> EmergencyServices { get; set; }
        public DbSet<LabRecordAnalysis> LabRecordAnalysis { get; set; }
        public DbSet<LabRecords> LabRecords { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<PrescriptionRecords> PrescriptionRecords { get; set; }
        public DbSet<AvailabilityDetails> AvailabilityDetails { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetails> AppointmentsDetails { get; set; }
        public DbSet<PrescriptionDetailTimings> PrescriptionDetailTimings { get; set; }
        public DbSet<PrescriptionDetails> PrescriptionDetails { get; set; }
        public DbSet<Localizations> Localizations { get; set; }
        public DbSet<Records> Records { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<DoctorQualifications> DoctorQualifacations {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
        .HasOne(a => a.Doctor)
        .WithMany(d => d.Appointments)
        .HasForeignKey(a => a.DoctorID)
        .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

            modelBuilder.Entity<DoctorRatings>()
                .HasOne(dr => dr.DoctorDetails)
                .WithMany(d => d.DoctorRatings)
                .HasForeignKey(dr => dr.DoctorID)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

            modelBuilder.Entity<PrescriptionRecords>()
                .HasOne(pr => pr.DoctorDetails)
                .WithMany()
                .HasForeignKey(pr => pr.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);
       
            modelBuilder.Entity<DoctorRatings>()
                .HasOne(d => d.DoctorDetails)
                .WithMany(d => d.DoctorRatings)
                .HasForeignKey(d => d.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PrescriptionRecords>()
        .HasOne(p => p.PatientDetails)
        .WithMany()
        .HasForeignKey(p => p.PatientID)
        .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

            modelBuilder.Entity<PrescriptionRecords>()
                .HasOne(p => p.DoctorDetails)
                .WithMany()
                .HasForeignKey(p => p.DoctorID)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

            modelBuilder.Entity<PrescriptionRecords>()
                .HasOne(p => p.Records)
                .WithMany()
                .HasForeignKey(p => p.RecordID)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

            modelBuilder.Entity<PrescriptionRecords>()
                .HasOne(p => p.Appointment)
                .WithMany()
                .HasForeignKey(p => p.AppointmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}
