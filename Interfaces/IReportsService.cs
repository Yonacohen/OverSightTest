using OverSightTest.Models;

namespace OverSightTest.Interfaces
{
    public interface IReportService
    {
        Response<List<CouponItem>> GetCouponsByUser(string userName);
        Response<List<CouponItem>> GetCouponsByDate(DateTime dt1,DateTime dt2);
        Response ExportToExcel();
    }
}
