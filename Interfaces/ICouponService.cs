using OverSightTest.Models;

namespace OverSightTest.Interfaces
{
    public interface ICouponService
    {
        Response<CouponItem> AddCoupon(CouponItem couponItem);
    }
}
