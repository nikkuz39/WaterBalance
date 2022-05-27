using Microsoft.EntityFrameworkCore;

namespace WaterBalance.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Consumer>? Consumers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=datafortables.db");
    }
}