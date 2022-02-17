using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models.Auth;
using StudentPlatformAPI.Services;
using Swashbuckle.AspNetCore.Filters;

namespace StudentPlatformAPI.Controllers
{
    /// <summary>
    ///     Register user and authentication
    /// </summary>
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

        /// <summary>
        ///     Create new user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignUp(UserSignUpDto dto)
        {
            var user = _mapper.Map<UserSignUpDto, User>(dto);

            var userCreateResult = await _userManager.CreateAsync(user, dto.Password);

            if (userCreateResult.Succeeded) return Created(string.Empty, string.Empty);

            return BadRequest(userCreateResult.Errors.First().Description);
        }

        /// <summary>
        ///     Authenticate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignIn(UserLoginDto dto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == dto.UserName);
            if (user is null) return NotFound("User not found");

            var userSigninResult = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (userSigninResult) return Ok(_tokenService.GenerateToken(user));

            return BadRequest("Email or password incorrect.");
        }

        #region UserLoginDtoExample

        public class UserLoginDtoExample : IExamplesProvider<UserLoginDto>
        {
            public UserLoginDto GetExamples()
            {
                return new UserLoginDto()
                {
                    UserName = "test",
                    Password = "zaq1@WSX"
                };
            }
        }

        #endregion

        #region UserSignUpDto

        public class UserSignUpDtoExample : IExamplesProvider<UserSignUpDto>
        {
            public UserSignUpDto GetExamples()
            {
                return new UserSignUpDto()
                {
                    UserName = "username",
                    FirstName = "Swag",
                    LastName = "Ger",
                    Email = "Swagger@Swagger.pl",
                    Password = "zaq1@WSX"
                };
            }
        }

        #endregion
    }
}