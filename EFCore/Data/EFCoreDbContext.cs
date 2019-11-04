using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options)
            : base(options)
        {

        }
        public DbSet<Models.School> Schools { get; set; }
        public DbSet<Models.Student> Students { get; set; }
    }
}