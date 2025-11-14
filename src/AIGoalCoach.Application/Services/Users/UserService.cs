using AIGoalCoach.Domain.Users;
using AIGoalCoach.Repositories.UsersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task AddUser(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserByEmailAddress(string emailAddress)
        {
            return await this._userRepository.GetUserByEmailAddress(emailAddress);
        }
    }
}
