using HepsiBurada.Domain;
using Microsoft.EntityFrameworkCore;

namespace HepsiBurada.Data.Context
{
    public class HepsiBuradaContext : DbContext
    {
        public HepsiBuradaContext(DbContextOptions<HepsiBuradaContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<AppTime> AppTimes { get; set; }
    }
}