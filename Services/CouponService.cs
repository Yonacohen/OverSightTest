using OverSightTest.Entities;
using OverSightTest.Interfaces;
using OverSightTest.Models;

namespace OverSightTest.Services
{
    public class CouponService : ICouponService
    {
        private readonly OversightDbContext _oversightDbContext;

        public CouponService(OversightDbContext oversightDbContext)
        {
            _oversightDbContext = oversightDbContext;
        }

        public Response<CouponItem> AddCoupon(CouponItem couponItem)
        {
            Response<CouponItem> response = new();

            if (couponItem.DiscountType == DiscountType.Percent &&
                (couponItem.Discount > 100 || couponItem.Discount <= 0))
            {
                response.SetError(Codes.InvalidArg, "Discount is not valid");
                return response;
            }

            Coupon coupon = new Coupon()
            {
                Id = Guid.NewGuid(),
                Discount = couponItem.Discount,
                Code = couponItem.Code,
                CreationDateTime = DateTime.Now,
                Description = couponItem.Description,
                DoublePromotions = couponItem.DoublePromotions,
                ExpiredDate = couponItem.ExpiredDate,
                IsLimited = couponItem.IsLimited,
                LimitedUseNum = couponItem.LimitedUseNum,

            };
            //_oversightDbContext.Coupons.Add()

            return response;
            //throw new NotImplementedException();
        }
    }
}
