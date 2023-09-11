using JuniorWeb.Api.CORE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorWeb.Api.Controllers
{
    public class TokenController : Controller
    {
        private readonly JwtManager _manager;

        public TokenController(JwtManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] TokenRequest request)
        {
            try
            {
                var token = _manager.MakeToken(request.UserName, request.Password);

                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    public class TokenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

