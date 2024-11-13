using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OverSightTest.Interfaces;
using OverSightTest.Models;

namespace OverSightTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService) //ctor
        {
            _couponService = couponService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCoupon([FromBody] CouponItem copounItem)
        {            
            var response = _couponService.AddCoupon(copounItem, User.Identity.Name);
            return response.ToActionResult();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCoupon([FromQuery] Guid id)
        {
            var response = _couponService.DeleteCoupon(id);
            return response.ToActionResult();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdataCoupon([FromBody] CouponItem couponItem)
        {
            var response = _couponService.UpdateCoupon(couponItem);
            return response.ToActionResult();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCoupons()
        {
            var response = _couponService.GetAllCoupons();
            return response.ToActionResult();
        }
        
        [HttpPut("order-price")]
        public IActionResult UpdateOrderPrice(string codeCoupon)
        {
            var response = _couponService.UpdateOrderPrice(codeCoupon);
            return response.ToActionResult();
        }
    }
}
