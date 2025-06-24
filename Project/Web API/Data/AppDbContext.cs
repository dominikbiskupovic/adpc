namespace WebApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Checkup> Checkups { get; set; }
        public DbSet<CheckupImage> CheckupImages { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");
                entity.Property(e => e.Id)              .HasColumnName("id");
                entity.Property(e => e.PersonalId)      .HasColumnName("personalId");
                entity.Property(e => e.Name)            .HasColumnName("name");
                entity.Property(e => e.Surname)         .HasColumnName("surname");
                entity.Property(e => e.DateOfBirth)     .HasColumnName("dateOfBirth").HasColumnType("date");
                entity.Property(e => e.Sex)             .HasColumnName("sex");
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.ToTable("MedicalRecord");
                entity.Property(e => e.Id)        .HasColumnName("id");
                entity.Property(e => e.PatientId) .HasColumnName("patientId");

                entity.HasOne(m => m.Patient)
                    .WithOne(p => p.MedicalRecord)
                    .HasForeignKey<MedicalRecord>(m => m.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.ToTable("Disease");
                entity.Property(e => e.Id)              .HasColumnName("id");
                entity.Property(e => e.MedicalRecordId).HasColumnName("medicalRecordId");
                entity.Property(e => e.Name)            .HasColumnName("name");
                entity.Property(e => e.StartDate)       .HasColumnName("startDate");
                entity.Property(e => e.EndDate)         .HasColumnName("endDate");
            });

            modelBuilder.Entity<Checkup>(entity =>
            {
                entity.ToTable("Checkup");
                entity.Property(e => e.Id)        .HasColumnName("id");
                entity.Property(e => e.PatientId) .HasColumnName("patientId");
                entity.Property(e => e.DoctorId)  .HasColumnName("doctorId");
                entity.Property(e => e.Date)      .HasColumnName("date").HasColumnType("date");
                entity.Property(e => e.Type)
                      .HasColumnName("type")
                      .HasColumnType("procedure_code");

                entity.HasMany(e => e.Images)
                      .WithOne(i => i.Checkup)
                      .HasForeignKey(i => i.CheckupId);
            });

            modelBuilder.Entity<CheckupImage>(entity =>
            {
                entity.ToTable("CheckupImage");
                entity.Property(e => e.Id)        .HasColumnName("id");
                entity.Property(e => e.CheckupId) .HasColumnName("checkupId");
                entity.Property(e => e.FileUrl)   .HasColumnName("fileUrl");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");
                entity.Property(e => e.Id)        .HasColumnName("id");
                entity.Property(e => e.Name)      .HasColumnName("name");
                entity.Property(e => e.CheckupId) .HasColumnName("checkupId");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");
                entity.Property(e => e.Id)       .HasColumnName("id");
                entity.Property(e => e.UserId)   .HasColumnName("userId");
                entity.Property(e => e.FullName) .HasColumnName("fullName");
            });

            modelBuilder.Entity<DoctorPatient>(entity =>
            {
                entity.ToTable("DoctorPatient");
                entity.Property(e => e.Id)        .HasColumnName("id");
                entity.Property(e => e.DoctorId)  .HasColumnName("doctorId");
                entity.Property(e => e.PatientId) .HasColumnName("patientId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(e => e.Id)       .HasColumnName("id");
                entity.Property(e => e.Username) .HasColumnName("username");
                entity.Property(e => e.Password) .HasColumnName("password");
                entity.Property(e => e.IsAdmin)  .HasColumnName("isAdmin");
            });
        }
    }
}