using Microsoft.EntityFrameworkCore;
using Numus.Server.Entities;

namespace Numus.Server.Data
{
    public class NumusServerContext : DbContext
    {
        public NumusServerContext(DbContextOptions<NumusServerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
