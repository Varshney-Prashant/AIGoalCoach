using AIGoalCoach.Application.Configurations;
using AIGoalCoach.Domain.Goals.Dtos;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Clients
{
    public class AIClientService : IAIClientService
    {
        private readonly IConfiguration _configuration;
        private readonly IChatClient _chatClient;

        public AIClientService(IConfiguration configuration, IChatClient chatClient)
        {
            this._configuration = configuration;
            this._chatClient = chatClient;
        }

        public async Task<RefinedGoalResponse> RefineRawGoal(RefineGoalRequest request)
        {
            var userPrompt = $"Goal: {request.RawGoal}";
            var model = _configuration["AI:ChatModel"];

            if (model == null)
            {
                throw new Exception("OpenAI:ChatModel configuration is missing.");
            }

            try
            {
                IntentType intent = request.RawGoal.ClassifyIntent();
                switch (intent)
                {
                    case IntentType.Goal:
                        return await GetRefinedGoalResponseAsync(userPrompt);
                    case IntentType.Unclear:
                        return new RefinedGoalResponse
                        {
                            IsGoalRefined = false,
                            Message = "Please enter a meaningful goal.",
                            GoalTasks = new List<string>()
                        };
                    default:
                        return new RefinedGoalResponse
                        {
                            ActionableGoal = "Please enter a goal you would like refining",
                            GoalTasks = new List<string>()
                        };
                }
            }
            catch( Exception ex)
            {
                throw new Exception("Error during OpenAI chat completion.", ex);
            }
            
        }

        private async Task<RefinedGoalResponse> GetRefinedGoalResponseAsync(string userPrompt)
        {
            var prompt = $@"
                    You are a professional OKR (Objectives and Key Results) assistant.

                    ### USER GOAL:
                    {userPrompt}

                    ### TASK:
                    1. Rewrite the goal into an *Actionable SMART Goal*  
                    2. Generate **3 to 5 measurable Key Results**  
                    3. Respond ONLY in valid JSON using the schema:

                    {{
                        ""actionableGoal"": ""string"",
                        ""goalTasks"": [
                            ""string"",
                            ""string"",
                            ""string""
                        ]
                    }}
            ";

            var responseMessage = await this._chatClient.GetResponseAsync([
                new (ChatRole.System, prompt),
                new (ChatRole.User, userPrompt)
                ], new ChatOptions()
                {
                    ResponseFormat = Microsoft.Extensions.AI.ChatResponseFormat.Json,
                    MaxOutputTokens = 512,
                    Temperature = 0.5f,
                    TopP = 0.95f
                });

            var rawText = this.CleanJson(responseMessage.Text);
            var aiResponse =  System.Text.Json.JsonSerializer
            .Deserialize<RefinedGoalAIResponse>(rawText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            return new RefinedGoalResponse()
            {
                IsGoalRefined = true,
                Message = string.Empty,
                ActionableGoal = aiResponse.ActionableGoal,
                GoalTasks = aiResponse.GoalTasks
            };
        }

        private string CleanJson(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            // Remove Markdown fences
            text = text.Replace("```json", "").Replace("```", "").Trim();

            // Remove leading/trailing junk
            int firstBrace = text.IndexOf("{");
            int lastBrace = text.LastIndexOf("}");

            if (firstBrace >= 0 && lastBrace >= 0)
                text = text.Substring(firstBrace, lastBrace - firstBrace + 1);

            return text;
        }
    }
}
