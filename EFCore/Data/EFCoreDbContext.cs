using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options)
            : base(options)
        {

        }
        public DbSet<Models.School> School { get; set; }
        public DbSet<Models.Student> Student { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.School>()
                        .Property(p => p.CreateTime)
                        .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Models.Student>()
                        .Property(p => p.CreateTime)
                        .HasDefaultValueSql("getdate()");
        }
    }
}