using OverSightTest.Models;

namespace OverSightTest.Interfaces
{
    public interface IReportService
    {
        Response<List<CouponItem>> GetCouponsByUser(Guid userId);
        Response<List<CouponItem>> GetCouponsByDate(DateTime dt1,DateTime dt2);       
    }
}
