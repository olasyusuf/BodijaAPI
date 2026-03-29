using Microsoft.EntityFrameworkCore;
using BodijaApi.Models;

namespace BodijaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseOrder> Orders { get; set; }
    }
}


