﻿using Microsoft.EntityFrameworkCore;
using OverSightTest.Entities;
using OverSightTest.Interfaces;
using OverSightTest.Models;
using System.Collections.Generic;

namespace OverSightTest.Services
{
    public class CouponService : ICouponService
    {
        private readonly OversightDbContext _oversightDbContext;

        public CouponService(OversightDbContext oversightDbContext)
        {
            _oversightDbContext = oversightDbContext;
        }

        public Response<CouponItem> AddCoupon(CouponItem couponItem, string userName)
        {
            Response<CouponItem> response = new();

            if (couponItem.DiscountType == DiscountType.Percent &&
                (couponItem.Discount > 100 || couponItem.Discount <= 0))
            {
                response.SetError(Codes.InvalidArg, "Discount is not valid");
                return response;
            }

            var dbCoupon = _oversightDbContext.Coupons.SingleOrDefault(c => c.Code == couponItem.Code);
            if (dbCoupon != null)//this coupon code is already exists
            {
                response.SetError(Codes.GeneralError, "This coupon code is already exists");
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
                DiscountType = couponItem.DiscountType,
                UserName = userName
            };

            _oversightDbContext.Coupons.Add(coupon);
            _oversightDbContext.SaveChanges();

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
                _oversightDbContext.SaveChanges();
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
            dbCoupon.Code = couponItem.Code;
            dbCoupon.Description = couponItem.Description;
            dbCoupon.IsLimited = couponItem.IsLimited;
            dbCoupon.LimitedUseNum = couponItem.LimitedUseNum;
            dbCoupon.Discount = couponItem.Discount;
            dbCoupon.DoublePromotions = couponItem.DoublePromotions;
            dbCoupon.ExpiredDate = couponItem.ExpiredDate;
            dbCoupon.DiscountType = couponItem.DiscountType;
            dbCoupon.CreationDateTime = DateTime.Now;

            //update all RELEVANT properties

            _oversightDbContext.SaveChanges();
            return response;
        }
        public Response<List<CouponItem>> GetAllCoupons()
        {
            var response = new Response<List<CouponItem>>();
            List<CouponItem> couponItems = new();

            try
            {
                var coupons = _oversightDbContext.Coupons.ToList();
                foreach (var coupon in coupons)
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
                        CreationDateTime = coupon.CreationDateTime
                    });
                }

                response.Result = couponItems;
            }
            catch (Exception ex)
            {
                response.SetError(Codes.GeneralError, "error in get all coupons");
            }
            return response;
        }
        public Response<float> UpdateOrderPrice(string codeCoupon)
        {
            var response = new Response<float>();
            Coupon? dbCoupon = _oversightDbContext.Coupons.SingleOrDefault(c => c.Code == codeCoupon);
            if (dbCoupon == null)
            {
                response.SetError(Codes.InvalidArg, "error in update order price");
                return response;
            }

            if (DateTime.Now > dbCoupon.ExpiredDate)
            {
                response.SetError(Codes.GeneralError, "coupon is expired");
                return response;
            }

            int orderPrice = 100;
            if (dbCoupon.DiscountType == DiscountType.ILS)
            {
                response.Result = orderPrice - dbCoupon.Discount;
            }
            else
            {
                response.Result = orderPrice * (100 - dbCoupon.Discount) / 100;
            }

            return response;
        }
    }
}

