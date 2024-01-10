using Microsoft.EntityFrameworkCore;

namespace lab4_2.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        
        public DbSet<Composition> Compositions { get; set; }
        
    }

}
