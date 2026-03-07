using Microsoft.EntityFrameworkCore;
using AppFE.Shared;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Empresa> empresas { get; set; }
    }
}