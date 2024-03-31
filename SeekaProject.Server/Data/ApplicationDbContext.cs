using Microsoft.EntityFrameworkCore;
using SeekaProject.Server.Models;

namespace SeekaProject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options ): base( options )
        {
        }

        public DbSet<SeekaProject.Server.Models.Product> Product { get; set; }
    }
}
