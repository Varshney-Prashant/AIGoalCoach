using AIGoalCoach.Domain.Users;
using AIGoalCoach.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Repositories.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await  _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAddress(string emailAddress)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }
    }
}
