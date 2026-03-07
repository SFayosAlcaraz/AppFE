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