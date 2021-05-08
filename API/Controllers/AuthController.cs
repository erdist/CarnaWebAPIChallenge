using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Authorization;
using Application.Helpers;
using Application.Services;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly JWTService _jwtservice;

        public AuthController(JWTService jwtservice)
        {
            _jwtservice = jwtservice;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Unit>> Register(AuthAdmin AuthAdmin)
        {
            return Ok(await Mediator.Send(new Register.Command { AuthAdmin = AuthAdmin }));
        }

        [HttpPost("login")]
        public async Task<ActionResult<Unit>> Login(AuthAdmin AuthAdmin)
        {
            var response = await Mediator.Send(new Login.Query { AuthAdmin = AuthAdmin });
            var jwtstring = "";
            if (response.Data == null)
            {

                return Ok(new { response, jwtstring });
            }
            jwtstring = _jwtservice.Generate(response.Data.Id);

            // Response.Cookies.Append("jwt", jwtstring, new CookieOptions
            // {
            //     HttpOnly = true
            // });

            return Ok(new { response, jwtstring });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                // Response.Cookies.Delete("jwt");
                return Ok(new { message = "Success!" });
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Logout failed!" });
            }
        }

        [HttpGet("{jwt}")]
        public async Task<ActionResult<AuthAdmin>> Authenticate(string jwt)
        {
            try
            {
                // var jwtstring = Request.Cookies["jwt"];
                var jwtstring = jwt;
                var token = _jwtservice.Verify(jwt);
                var id = int.Parse(token.Claims.First(x => x.Type == "id").Value);

                var admin = await Mediator.Send(new GetAdmin.Query { Id = id });
                var response = TResponse<AuthAdmin>.GetResult(1, "Authentication Successful", admin);
                response.Data.Password = "";
                return Ok(response);
            }
            catch (System.Exception)
            {
                var response = TResponse<AuthAdmin>.GetResult(0, "Authentication Failed", null);
                return Ok(response);
            }
        }
    }
}