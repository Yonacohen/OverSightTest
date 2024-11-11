using OverSightTest.Models;

namespace OverSightTest.Interfaces
{
    public interface ICouponService
    {
        Response<CouponItem> AddCoupon(CouponItem couponItem);
        Response DeleteCoupon(Guid couponItemId);
        Response<CouponItem> UpdateCoupon(CouponItem couponItem);
        Response<List<CouponItem>> GetAllCoupons();
    }
}
