using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models.Auth;
using StudentPlatformAPI.Services;

namespace StudentPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;


        public AuthController(IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("SignUp")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadGateway)]
        public async Task<IActionResult> SignUp(UserSignUpDto dto)
        {
            var user = _mapper.Map<UserSignUpDto, User>(dto);

            var userCreateResult = await _userManager.CreateAsync(user, dto.Password);

            if (userCreateResult.Succeeded) return Created(string.Empty, string.Empty);

            return BadRequest(userCreateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpGet("Detail")]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            if (user == null) return NotFound();
            var userDto = _mapper.Map<User, UserDto>(user);

            return Ok(userDto);
        }


        [Authorize]
        [HttpPut("UpdateUserDetails")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            var userUpdateResult = await _userManager.UpdateAsync(user);

            if (userUpdateResult.Succeeded) return NoContent();

            return BadRequest(userUpdateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpPut("UpdateUserEmail")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateUserEmail(UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, dto.Email);
            var userUpdateResult = await _userManager.ChangeEmailAsync(user, dto.Email, token);

            if (userUpdateResult.Succeeded) return NoContent();

            return BadRequest(userUpdateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpPut("UpdateUserPassword")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateUserPassword(UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            var userUpdateResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (userUpdateResult.Succeeded) return NoContent();

            return BadRequest(userUpdateResult.Errors.First().Description);
        }


        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadGateway)]
        public async Task<IActionResult> SignIn(UserLoginDto dto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == dto.UserName);
            if (user is null) return NotFound("User not found");

            var userSigninResult = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (userSigninResult) return Ok(_tokenService.GenerateToken(user));

            return BadRequest("Email or password incorrect.");
        }

        [Authorize]
        [HttpPut("UpdateUserName")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateUserName(UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value);
            var userUpdateResult = await _userManager.SetUserNameAsync(user, dto.UserName);

            if (userUpdateResult.Succeeded)
            {
                await _userManager.UpdateNormalizedUserNameAsync(user);
                return NoContent();
            }

            return BadRequest(userUpdateResult.Errors.First().Description);
        }
    }
}