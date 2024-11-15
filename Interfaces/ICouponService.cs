﻿using OverSightTest.Models;

namespace OverSightTest.Interfaces
{
    public interface ICouponService
    {
        Response<CouponItem> AddCoupon(CouponItem couponItem, string userName);
        Response DeleteCoupon(Guid couponItemId);
        Response<CouponItem> UpdateCoupon(CouponItem couponItem);
        Response<List<CouponItem>> GetAllCoupons();
        Response<float> UpdateOrderPrice(string codeCoupon);

    }
}
