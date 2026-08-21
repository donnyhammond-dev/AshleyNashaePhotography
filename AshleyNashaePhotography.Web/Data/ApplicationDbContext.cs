using Microsoft.EntityFrameworkCore;
using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HomePage> HomePages { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<PhotoSession> PhotoSessions { get; set; }

    }
}
