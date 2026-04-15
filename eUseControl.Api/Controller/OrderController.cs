using eUseControl.Business;
using eUseControl.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eUseControl.Api.Controller
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderBusiness _orderBusiness;

        public OrderController(OrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_orderBusiness.GetAll());
        }

        [HttpGet("my")]
        public IActionResult GetMyOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(_orderBusiness.GetByUser(userId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var order = _orderBusiness.Create(userId, req);
                return StatusCode(201, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
