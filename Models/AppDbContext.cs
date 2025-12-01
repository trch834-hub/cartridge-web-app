using Microsoft.EntityFrameworkCore;
using CartridgeWebApp.Models;

namespace CartridgeWebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cartridge> Cartridges { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}