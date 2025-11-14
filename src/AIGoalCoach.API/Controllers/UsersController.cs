using AIGoalCoach.Application.Services.Tokens;
using AIGoalCoach.Application.Services.Users;
using AIGoalCoach.Domain.Users;
using AIGoalCoach.Domain.Users.Dtos;
using AIGoalCoach.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIGoalCoach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly ITokenService TokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            this.UserService = userService;
            this.TokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<UserLoginResponse> Login([FromBody] UserLoginRequest request)
        {
            var user = await this.UserService.GetUserByEmailAddress(request.EmailAddress);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    IsSuccess = false,
                    AccessToken = null,
                    Message = "User does not exist"
                };
            }

            if (!request.Password.VerifyPassword(user.PasswordHash))
            {
                return new UserLoginResponse
                {
                    IsSuccess = false,
                    AccessToken = null,
                    Message = "Invalid Password!"
                };
            }

            var token = this.TokenService.GenerateToken(user);

            return new UserLoginResponse
            {
                IsSuccess = true,
                AccessToken = token,
                Message = "Login Successful"
            };
        }

        [HttpPost("register")]
        public async Task<bool> AddUserAsync([FromBody] UserRegisterRequest user)
        {
            var existingUser = await this.UserService.GetUserByEmailAddress(user.EmailAddress);
            if (existingUser != null)
            {
                return false;
            }

            var newUser = new User
            {
                EmailAddress = user.EmailAddress,
                Username = user.UserName
            };

            newUser.PasswordHash = user.Password.HashPassword();
            await this.UserService.AddUser(newUser);
            return true;
        }
    }
}
