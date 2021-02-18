using Hahn.ApplicatonProcess.December2020.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Models
{
   
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext(
            DbContextOptions<ApplicantDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Applicant> Applicant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>()
                .HasKey(x => x.Id);
        }
    }
}
