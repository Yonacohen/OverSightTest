using OverSightTest.Models;

namespace OverSightTest.Entities
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description {  get; set; }

        public string? UserName {  get; set; }
        public DateTime CreationDateTime { get; set; }
        public float Discount {  get; set; }
        public DiscountType DiscountType { get; set; }

        public DateTime ExpiredDate { get; set; }
        public bool DoublePromotions { get; set; }
        public bool IsLimited {  get; set; }  
        public int? LimitedUseNum { get; set; }   


    }

}
