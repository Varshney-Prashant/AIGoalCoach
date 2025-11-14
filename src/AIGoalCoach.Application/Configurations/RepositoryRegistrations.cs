using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGoalCoach.Repositories.GoalsRepository;
using AIGoalCoach.Repositories.UsersRepository;
using Microsoft.Extensions.DependencyInjection;


namespace AIGoalCoach.Application.Configurations
{
    public static class RepositoryRegistrations
    {
        public static void AddRepositoryRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
