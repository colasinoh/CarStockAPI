using Car_Stock_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Car_Stock_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasKey(x => x.Id);
        }
    }
}
