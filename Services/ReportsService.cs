using OverSightTest.Entities;
using OverSightTest.Interfaces;
using OverSightTest.Models;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.EntityFrameworkCore;

namespace OverSightTest.Services
{
    public class ReportsService : IReportService
    {
        private readonly OversightDbContext _oversightDbContext;

        public ReportsService(OversightDbContext oversightDbContext)
        {
            _oversightDbContext = oversightDbContext;
        }

        public Response<List<CouponItem>> GetCouponsByUser(string userName)
        {
            Response<List<CouponItem>> response = new();

            try
            {
                var couponList = _oversightDbContext.Coupons.Where(coupon => coupon.UserName == userName).ToList();
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
        public Response ExportToExcel()
        {
            Response response = new();
            // Set up EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a worksheet to the Excel file
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Coupons");

                // Define headers
                worksheet.Cells[1, 1].Value = "Code";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "CreationDateTime";
                worksheet.Cells[1, 4].Value = "ExpiredDate";
                worksheet.Cells[1, 5].Value = "Discount";
                worksheet.Cells[1, 6].Value = "DoublePromotions";
                worksheet.Cells[1, 7].Value = "IsLimited";
                worksheet.Cells[1, 8].Value = "LimitedUseNum";

                // Fetch coupons from the database
                List<Coupon> coupons = _oversightDbContext.Coupons.ToList();

                // Populate the worksheet with coupon data
                for (int i = 0; i < coupons.Count; i++)
                {
                    var coupon = coupons[i];
                    worksheet.Cells[i + 2, 1].Value = coupon.Code.ToString();
                    worksheet.Cells[i + 2, 2].Value = coupon.Description;
                    worksheet.Cells[i + 2, 3].Value = coupon.CreationDateTime.ToShortDateString();
                    worksheet.Cells[i + 2, 4].Value = coupon.ExpiredDate.ToShortDateString();
                    worksheet.Cells[i + 2, 5].Value = coupon.Discount;
                    worksheet.Cells[i + 2, 6].Value = coupon.DoublePromotions ? "Yes" : "No";
                    worksheet.Cells[i + 2, 7].Value = coupon.IsLimited ? "Yes" : "No";
                    worksheet.Cells[i + 2, 8].Value = coupon.LimitedUseNum;
                }

                // Auto-fit all columns for better readability
                worksheet.Cells.AutoFitColumns();

                // Save the Excel file to the specified path
                FileInfo excelFile = new FileInfo("coupons.xlsx");
                package.SaveAs(excelFile);
            }

            return response;
        }
    }
}


