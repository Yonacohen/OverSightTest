
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using OverSightTest.Entities;

namespace OverSightTest
{
    public class OversightDbContext : DbContext

    {
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost; Database=OversightDb; Uid=yonatanc;Pwd = barzel2025;",           
                    new MySqlServerVersion(new Version(9, 1, 0)) // specify MySQL version here
                );
            }
        }       
    }
}
