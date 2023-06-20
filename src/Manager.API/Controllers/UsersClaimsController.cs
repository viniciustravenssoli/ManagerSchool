using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin" )]
    [ApiController]
    public class ClaimsSetupController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ClaimsSetupController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager

        )
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return Ok(userClaims);
        }

        [HttpPost]
        [Route("AddClaimsToUser")]

        public async Task<IActionResult> AddClaimsToUser(string email, string claimName, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var userClaim = new Claim(claimName, claimValue);

            var result = await _userManager.AddClaimAsync(user, userClaim);

            if(result.Succeeded)
            {
                return Ok(new {
                    result = $"User {user.Email} has a claim {claimName} added to them"
                });
            }

            return BadRequest(new {
                error = $"Unable to add claim {claimName} to the user {user.Email}"
            });
        }

    }
}