using System;
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
using Swashbuckle.AspNetCore.Filters;

namespace StudentPlatformAPI.Controllers
{
    /// <summary>
    ///     Get and edit User info
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get User info
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     Update User email
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("Email")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserEmail(UserUpdateDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, dto.Email);
                var userUpdateResult = await _userManager.ChangeEmailAsync(user, dto.Email, token);

                if (userUpdateResult.Succeeded) return NoContent();

                throw new Exception(userUpdateResult.Errors.First().Description);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Update User detail
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("Detail")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserName(UserUpdateDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;

                var userUpdateResult = await _userManager.SetUserNameAsync(user, dto.UserName);
                if (userUpdateResult.Succeeded) return NoContent();

                throw new Exception(userUpdateResult.Errors.First().Description);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Update User password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("Password")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserPassword(UserUpdateDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)
                    ?.Value);
                var userUpdateResult =
                    await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

                if (userUpdateResult.Succeeded) return NoContent();

                throw new Exception(userUpdateResult.Errors.First().Description);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #region UserUpdateDtoExample

        public class UserUpdateDtoExample : IExamplesProvider<UserUpdateDto>
        {
            public UserUpdateDto GetExamples()
            {
                return new UserUpdateDto
                {
                    UserName = "test",
                    FirstName = "updated",
                    LastName = "user",
                    Email = "update@user.pl",
                    NewPassword = "zaq1@WSX",
                    CurrentPassword = "zaq1@WSX"
                };
            }
        }

        #endregion
    }
}