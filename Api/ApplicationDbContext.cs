using Microsoft.EntityFrameworkCore;
using Shared; // Asegúrate de que coincida con el nombre de tu proyecto

namespace AppFE.Api.Data // <--- Fíjate bien en este nombre
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