
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
                    "Server=localhost; Database=OversightDb; Uid=yonatanc;Pwd = barzel2025; ",
                    //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
                    //Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
                    new MySqlServerVersion(new Version(9, 1, 0)) // specify MySQL version here
                );
            }
        }       
    }
}
