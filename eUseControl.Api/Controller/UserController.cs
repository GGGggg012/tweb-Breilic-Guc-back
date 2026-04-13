using eUseControl.Business;
using eUseControl.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;

        public UserController(UserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userBusiness.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userBusiness.GetById(id);
            if (user == null)
                return NotFound(new { message = $"User {id} not found" });
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RegisterRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userBusiness.Update(id, req);
            if (result == null)
                return NotFound(new { message = $"User {id} not found" });
            return Ok(result);
        }
    }
}