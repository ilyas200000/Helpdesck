using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service_Auth.Models;

namespace Service_Auth.Controller
    {
        [ApiController]
        [Route("api/user/")]
        public class UserController : ControllerBase
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public UserController(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Roles = model.Roles,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok("User registered successfully");
                }

                return BadRequest(result.Errors);
            }
        }
    }


