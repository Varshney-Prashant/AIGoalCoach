using AIGoalCoach.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Services.Tokens
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
