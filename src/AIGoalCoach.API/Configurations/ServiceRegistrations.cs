using AIGoalCoach.Application.Clients;
using AIGoalCoach.Application.Configurations;
using AIGoalCoach.Application.Services.Goals;
using AIGoalCoach.Application.Services.Tokens;
using AIGoalCoach.Application.Services.Users;
using AIGoalCoach.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.ClientModel;

namespace AIGoalCoach.API.Configurations
{
    public static class ServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("GoalDb"));
            });

            services.AddRepositoryRegistrations();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAzureOpenAIService, AzureOpenAIService>();
            services.AddScoped<IGoalService, GoalService>();
        }

        public static void AddAzureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var openAIEndpoint = configuration["OpenAI:Endpoint"];
            var openAIApiKey = configuration["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(openAIEndpoint) || string.IsNullOrEmpty(openAIApiKey))
            {
                throw new Exception("OpenAI configuration is missing.");
            }

            services.AddSingleton(provider =>
            {
                var options = new OpenAI.OpenAIClientOptions
                {
                    Endpoint = new Uri(openAIEndpoint)
                };
                return new OpenAI.OpenAIClient(new ApiKeyCredential(openAIApiKey), options);
            });
        }
    }
}
