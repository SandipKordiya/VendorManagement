using Vendor.Database.Identity;
using VendorAPI.Dtos;
using VendorAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace VendorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Vendor.Database.Identity.User> _userManager;
        private readonly SignInManager<Vendor.Database.Identity.User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<Vendor.Database.Identity.User> userManager, SignInManager<Vendor.Database.Identity.User> signInManager,
            ILogger<AccountController> logger, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDto registerDto)
        {
            //if (CheckEmailExistsAsync(registerDto.Register.Email).Result.Value)
            //{
            //    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            //}

            var user = new Vendor.Database.Identity.User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.Mobile,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                NormalizedUserName = registerDto.UserName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

            var result2 = await _userManager.AddToRoleAsync(user, "Vendor");

            if (!result2.Succeeded) return BadRequest();

            return Ok();
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            var userReturn = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                UserName = user.UserName,
                Roles = roles.First()
            };

            return userReturn;
        }
    }
}
