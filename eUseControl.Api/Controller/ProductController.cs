using eUseControl.Business;
using eUseControl.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controller
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBusiness _productBusiness;

        public ProductController(ProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productBusiness.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productBusiness.GetById(id);
            if (product == null)
                return NotFound(new { message = $"Product {id} not found" });
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] ProductRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _productBusiness.Create(req);
            return StatusCode(201, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] ProductRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _productBusiness.Update(id, req);
            if (result == null)
                return NotFound(new { message = $"Product {id} not found" });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var deleted = _productBusiness.Delete(id);
            if (!deleted)
                return NotFound(new { message = $"Product {id} not found" });
            return NoContent();
        }
    }
}
