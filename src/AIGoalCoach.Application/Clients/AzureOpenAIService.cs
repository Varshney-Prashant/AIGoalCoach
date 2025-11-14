using AIGoalCoach.Domain.Goals.Dtos;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Clients
{
    public class AzureOpenAIService : IAzureOpenAIService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly IConfiguration _configuration;

        public AzureOpenAIService(OpenAIClient openAIClient, IConfiguration configuration)
        {
            this._openAIClient = openAIClient;
            this._configuration = configuration;
        }

        public async Task<RefinedGoalResponse> RefineRawGoal(RefineGoalRequest request)
        {
            var prompt = $@"
                    You are a professional OKR (Objectives and Key Results) assistant.

                    ### USER GOAL:
                    {request.RawGoal}

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

            var model = _configuration["OpenAI:ChatModel"];

            if (model == null)
            {
                throw new Exception("OpenAI:ChatModel configuration is missing.");
            }

            try
            {
                List<ChatMessage> messages = new List<ChatMessage>
                {
                    new SystemChatMessage("You are a helpful assistant that helps users refine their goals into actionable SMART goals with measurable key results."),
                    new UserChatMessage(prompt)
                };

                var chatClient = this._openAIClient.GetChatClient(model);
                ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages, new ChatCompletionOptions
                {
                    MaxOutputTokenCount = 512,
                    Temperature = 0.7f,
                    TopP = 0.95f
                });

                var responseMessage = chatCompletion.Content[0].Text;

                return System.Text.Json.JsonSerializer
                .Deserialize<RefinedGoalResponse>(responseMessage)!;
            }
            catch( Exception ex)
            {
                throw new Exception("Error during OpenAI chat completion.", ex);
            }
            
        }
    }
}
