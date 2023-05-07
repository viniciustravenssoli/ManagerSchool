using System;
using System.Linq;
using System.Threading.Tasks;
using Manager.API.Token;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Manager.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator, UserManager<IdentityUser> userManager, ManagerContext context)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // We can utilise the model
                    var existingUser = await _userManager.FindByEmailAsync(user.Email);

                    if (existingUser != null)
                    {
                        return BadRequest(new ResultViewModel()
                        {
                            Message = "Email already in use",
                            Success = false,
                            Data = null
                        });
                    }

                    var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };

                    var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                    if (isCreated.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "BasicUser");

                        var jwtToken = await _tokenGenerator.GenerateJwtToken(newUser);

                        return Ok(jwtToken);
                    }
                    
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Success = false
                        });
                    
                }

                return BadRequest(Responses.InvalidPayloud());
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors)); ;
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }


        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(user.Email);

                    if (existingUser == null)
                    {
                        return StatusCode(401, Responses.UnauthorizedErrorMessage());
                    }

                    var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                    if (!isCorrect)
                    {
                        return StatusCode(401, Responses.UnauthorizedErrorMessage());
                    }

                    var jwtToken = await _tokenGenerator.GenerateJwtToken(existingUser);

                    return Ok(jwtToken);
                }

                return BadRequest(Responses.InvalidPayloud());
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors)); ;
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _tokenGenerator.VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                {
                    return BadRequest(Responses.InvalidPayloud());
                }

                return Ok(result);
            }

            return BadRequest(Responses.InvalidPayloud());
        }
    }
}