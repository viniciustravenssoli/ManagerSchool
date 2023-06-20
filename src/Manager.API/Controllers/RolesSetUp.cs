using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manager.API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin" )]
    public class RolesSetUp : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesSetUp(
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    return Ok(new
                    {
                        result = $"The new role '{roleName}' has been added successfully"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        result = $"The role {roleName} has not been added"
                    });
                }
            }

            return BadRequest(new { error = "Role alredy exist" });
        }

        [HttpGet]
        [Route("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Route("AddUserToRole")]

        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success, user has been added to the role"
                });
            }
            else
            {
                return BadRequest(new
                {
                    error = "The user was not able to be added to the role"
                });
            }

        }
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost]
        [Route("RemoveUserFromRole")]

        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if(result.Succeeded)
            {
                return Ok(new {
                    result = $"User with email {email} has been removed from role {roleName}"
                });
            }

            return BadRequest(new {
                error = $"Unable to remove User {email} from role {roleName}"
            });
        }
    }
}