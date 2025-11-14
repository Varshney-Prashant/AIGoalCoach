using AIGoalCoach.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Repositories.UsersRepository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<User> GetUserByEmailAddress(string emailAddress);
    }
}
