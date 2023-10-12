using Vendor.Database.Identity;
using Vendor.Database.Repository;
using VendorAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VendorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger,
            UserManager<Vendor.Database.Identity.User> userManager,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _userRepository = userRepository;
        }


        [HttpGet(nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] UserQueryParamsDTO userQueryParams)
        {
            try
            {
                var result = await _userRepository.GetUsers(userQueryParams.PageNumber, userQueryParams.PageSize);               
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUsers error: {Message}", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet(nameof(GetUser) + "/{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            try
            {
                var result = await _userRepository.GetUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUser error: {Message}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(RegisterUserDto registerDto)
        {
            if (await _userRepository.CheckUserExist(registerDto.Email))
                return BadRequest("Email already exist.");

            var user = new Vendor.Database.Identity.User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.Mobile,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                NormalizedUserName = registerDto.UserName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Address = registerDto.Address,
                City = registerDto.City,
                State = registerDto.State,
                Description = registerDto.Description,
                PanNumber = registerDto.PanNumber,
                BotEmail = registerDto.BotEmail,
                CompanyCode = registerDto.CompanyCode
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

            var result2 = await _userManager.AddToRoleAsync(user, "Vendor");

            if (!result2.Succeeded) return BadRequest();

            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(string id, RegisterUserDto registerDto)
        {

            var result = await _userRepository.GetUser(id);

            result.FirstName = registerDto.FirstName;
            result.LastName = registerDto.LastName;
            result.PhoneNumber = registerDto.Mobile;
            result.NormalizedUserName = registerDto.UserName;
            result.Email = registerDto.Email;
                result.UserName = registerDto.UserName;
            result.Address = registerDto.Address;
            result.City = registerDto.City;
                result.State = registerDto.State;
            result.Description = registerDto.Description;
            result.PanNumber = registerDto.PanNumber;
            result.BotEmail = registerDto.BotEmail;
            result.CompanyCode = registerDto.CompanyCode;
           

            var res = await _userRepository.UpdateUser(id, result);

            if (!res) return BadRequest();


            return Ok();
        }
    }
}
