using eUseControl.Business;
using eUseControl.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_orderBusiness.GetByUser(User));
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _orderBusiness.Create(User, req);
            return StatusCode(201, order);
        }
    }
}
