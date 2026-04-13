using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eUseControl.Api.Filters
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cfg = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]));
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = cfg["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = cfg["Jwt:Audience"]
                }, out var validatedToken);

                var jwt = (JwtSecurityToken)validatedToken;
                var role = jwt.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                if (role != "Admin")
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
