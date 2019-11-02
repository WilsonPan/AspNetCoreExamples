

using Microsoft.EntityFrameworkCore;

namespace Razor.Data
{
    public class RazorDbContext : DbContext
    {
        public RazorDbContext(DbContextOptions<RazorDbContext> options)
            : base(options)
        {

        }

        public DbSet<Razor.Models.Book> Book { get; set; }
    }
}