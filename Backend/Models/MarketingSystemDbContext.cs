using Microsoft.EntityFrameworkCore;

namespace MarketingSystem.Backend.Models
{
    public class MarketingSystemDbContext : DbContext
    {
        public MarketingSystemDbContext(DbContextOptions<MarketingSystemDbContext> context) : base(context)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}