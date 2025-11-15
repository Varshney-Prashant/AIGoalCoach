using AIGoalCoach.Application.Clients;
using AIGoalCoach.Application.Configurations;
using AIGoalCoach.Application.Services.Goals;
using AIGoalCoach.Application.Services.Tokens;
using AIGoalCoach.Application.Services.Users;
using AIGoalCoach.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OpenAI;
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
            services.AddScoped<IAIClientService, AIClientService>();
            services.AddScoped<IGoalService, GoalService>();
        }

        public static void AddAzureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IChatClient>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var providerName = config["AI:Provider"];
                var model = config["AI:ChatModel"];
                var endpoint = config["AI:Endpoint"];
                var apiKey = config["AI:ApiKey"];

                if (providerName == "Ollama")
                {
                    var client = new OllamaApiClient(new Uri(endpoint));
                    client.SelectedModel = model;
                    return client;
                }

                if (providerName == "OpenAI")
                {
                    var client = new OpenAIClient(apiKey).GetChatClient(model).AsIChatClient();
                }

                throw new Exception("Invalid AI provider in config.");
            });

        }
    }
}
