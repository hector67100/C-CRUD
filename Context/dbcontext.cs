using Microsoft.EntityFrameworkCore;
using Testcrud.Models;

namespace Testcrud.Data
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {

        protected readonly IConfiguration Configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public virtual DbSet<lista> Lista { get; set; }
        
    }
}