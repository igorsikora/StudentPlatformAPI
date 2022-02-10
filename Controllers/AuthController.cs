using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models.Auth;
using StudentPlatformAPI.Services;
using StudentPlatformAPI.Settings;

namespace StudentPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;


        public AuthController(IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;        
            _tokenService = tokenService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDto dto)
        {
            var user = _mapper.Map<UserSignUpDto, User>(dto);

            var userCreateResult = await _userManager.CreateAsync(user, dto.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return BadRequest(userCreateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpGet("Detail")]
        public IActionResult Get()
        {

            var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var userDto = _mapper.Map<User, UserDto>(user);

            return Ok(userDto);
        }



        [Authorize]
        [HttpPut("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateDto dto)
        {

            var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            var userUpdateResult = await _userManager.UpdateAsync(user);

            if (userUpdateResult.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(userUpdateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpPut("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail(UserUpdateDto dto)
        {
            var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, dto.Email);
            var userUpdateResult = await _userManager.ChangeEmailAsync(user, dto.Email, token);

            if (userUpdateResult.Succeeded)
            {
                var setUserName = await _userManager.SetUserNameAsync(user, dto.Email);
                if (setUserName.Succeeded)
                {
                    await _userManager.UpdateNormalizedUserNameAsync(user);
                    return NoContent();
                }

                return BadRequest(setUserName.Errors.First().Description);
            }

            return BadRequest(userUpdateResult.Errors.First().Description);
        }

        [Authorize]
        [HttpPut("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(UserUpdateDto dto)
        {

            var userId = new Guid(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            var userUpdateResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (userUpdateResult.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(userUpdateResult.Errors.First().Description);
        }
        

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginDto dto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == dto.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (userSigninResult)
            {
                return Ok(_tokenService.GenerateToken(user));
            }

            return BadRequest("Email or password incorrect.");
        }


    }
}
