using Microsoft.EntityFrameworkCore;
using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HomePageSettings> HomePageSettings { get; set; }

    }
}
