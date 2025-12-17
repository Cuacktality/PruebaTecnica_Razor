using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<SeekerProfile> SeekerProfile { get; set; } = default!;
        public DbSet<EmployerProfile> EmployerProfile { get; set; } = default!;
        public DbSet<JobOffer> JobOffer { get; set; } = default!;
        public DbSet<JobApplication> JobApplication { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.Seeker)
                .WithMany()
                .HasForeignKey(a => a.SeekerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.JobOffer)
                .WithMany()
                .HasForeignKey(a => a.JobOfferId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.JobOffer)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobOfferId);
        }
    }
}
