using CCSMDataManager.Entities;
using CCSMDataManager.Models;
using CCSMDataManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CCSMDataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        
        [HttpPost("register")] 
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("User already exists");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(token);
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticaticatedOnlyEndpoint()
        {
            return Ok("This endpoint is protected and requires authentication.");
        }
    }

}
