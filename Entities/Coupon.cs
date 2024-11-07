namespace OverSightTest.Entities
{
    public class Coupon
    {   
        Guid Id { get; set; }
        string Code { get; set; }
        string Description {  get; set; }

        Guid UserCreatorId {  get; set; }
        DateTime CreationDateTime { get; set; }
        float Discount {  get; set; }
        DateTime ExpiredDate { get; set; }
        bool DoublePromotions { get; set; }
        bool IsLimited {  get; set; }  
        int? LimitedUseNum { get; set; }   


    }

}
