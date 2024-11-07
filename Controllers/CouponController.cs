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
        public IActionResult CreateCoupon([FromBody] CouponItem copounItem)
        {
            _couponService.AddCoupon(copounItem);
            return Ok();
        }
    }
}
