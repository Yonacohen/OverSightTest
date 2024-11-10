using Microsoft.EntityFrameworkCore;
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

            _oversightDbContext.Coupons.Add(coupon);
            // _oversightDbContext.SaveChanges();

            response.Result = couponItem;
            return response;
        }

        public Response DeleteCoupon(Guid couponItemId)
        {
            Response response = new();
            try
            {
                Coupon? copoun = _oversightDbContext.Coupons.SingleOrDefault(c => c.Id == couponItemId);
                if (copoun == null)
                {
                    response.SetError(Codes.InvalidArg, $"coupon item id {couponItemId} not exist");
                    return response;
                }

                _oversightDbContext.Coupons.Remove(copoun);
                //_oversightDbContext.SaveChanges();              
            }
            catch (Exception ex)
            {
                response.SetError(Codes.GeneralError, "error in delete coupon");
            }

            return response;
        }
        public Response<CouponItem> UpdateCoupon(CouponItem couponItem)
        {
            Response<CouponItem> response = new();
            Coupon? dbCoupon = _oversightDbContext.Coupons.SingleOrDefault(c => c.Id == couponItem.Id);
            if (dbCoupon == null)
            {
                response.SetError(Codes.InvalidArg, $"coupon item id {couponItem.Id} not exist");
                return response;
            }

            dbCoupon.Description = couponItem.Description;
            dbCoupon.IsLimited= couponItem.IsLimited;
            dbCoupon.LimitedUseNum = couponItem.LimitedUseNum;    
            dbCoupon.Discount= couponItem.Discount;
            dbCoupon.DoublePromotions= couponItem.DoublePromotions;
            dbCoupon.ExpiredDate= couponItem.ExpiredDate;
            dbCoupon.DiscountType = couponItem.DiscountType;
            //update all RELEVANT properties

            _oversightDbContext.SaveChanges();
            return response;
        }
    }
}

