
using Microsoft.EntityFrameworkCore;
using OverSightTest.Entities;

namespace OverSightTest
{
    public class OversightDbContext : DbContext

    {
        public DbSet<Coupon> Coupons { get; set; }

    }
}
