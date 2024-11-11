using OverSightTest.Entities;
using OverSightTest.Interfaces;
using OverSightTest.Models;
using System.Collections.Generic;

namespace OverSightTest
{
    public class ReportsService : IReportService
    {
        private readonly OversightDbContext _oversightDbContext;

        public ReportsService(OversightDbContext oversightDbContext)
        {
            _oversightDbContext = oversightDbContext;
        }

        public Response<List<CouponItem>> GetCouponsByUser(Guid userId)
        {
            Response<List<CouponItem>> response = new();

            try
            {
                var couponList = _oversightDbContext.Coupons.Where(coupon => coupon.UserCreatorId == userId).ToList();
                List<CouponItem> couponItems = new();

                foreach (var coupon in couponList)
                {
                    couponItems.Add(new CouponItem()
                    {
                        Code = coupon.Code,
                        Id = coupon.Id,
                        Description = coupon.Description,
                        Discount = coupon.Discount,
                        DiscountType = coupon.DiscountType,
                        DoublePromotions = coupon.DoublePromotions,
                        ExpiredDate = coupon.ExpiredDate,
                        IsLimited = coupon.IsLimited,
                        LimitedUseNum = coupon.LimitedUseNum,
                    });
                }

                response.Result = couponItems;
            }
            catch (Exception)
            {
                response.SetError(Codes.GeneralError, "error in get coupon by user");
            }

            return response;
        }

        public Response<List<CouponItem>> GetCouponsByDate(DateTime fromDate, DateTime toDate)
        {
            Response<List<CouponItem>> response = new();
            try
            {
                //validate
                if (fromDate > DateTime.Now)
                {
                    response.SetError(Codes.GeneralError, "error in get couopn by date");
                    return response;
                }

                var couponList = _oversightDbContext.Coupons.Where(coupon => coupon.CreationDateTime >= fromDate
                                               && coupon.CreationDateTime <= toDate).ToList();

                List<CouponItem> couponItems = new();

                foreach (var coupon in couponList)
                {
                    couponItems.Add(new CouponItem()
                    {
                        Code = coupon.Code,
                        Id = coupon.Id,
                        Description = coupon.Description,
                        Discount = coupon.Discount,
                        DiscountType = coupon.DiscountType,
                        DoublePromotions = coupon.DoublePromotions,
                        ExpiredDate = coupon.ExpiredDate,
                        IsLimited = coupon.IsLimited,
                        LimitedUseNum = coupon.LimitedUseNum,
                    });
                }

                response.Result = couponItems;
            }
            catch (Exception ex)
            {
                response.SetError(Codes.GeneralError, "error in get coupons by date");
            }
            return response;
        }

       
    }
}

