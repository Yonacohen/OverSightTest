namespace OverSightTest.Models
{
    public class CouponItem
    {
        
        public string Code { get; set; }
        public string Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public float Discount { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool DoublePromotions { get; set; }
        public bool IsLimited { get; set; }
        public int? LimitedUseNum { get; set; }
    }

    public enum DiscountType
    {
        ILS,
        Percent
    }
}
