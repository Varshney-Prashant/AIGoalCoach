using AIGoalCoach.Application.Clients;
using AIGoalCoach.Domain.Goals;
using AIGoalCoach.Domain.Goals.Dtos;
using AIGoalCoach.Repositories.GoalsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Services.Goals
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IAzureOpenAIService _azureOpenAIService;

        public GoalService(IGoalRepository goalRepository, IAzureOpenAIService openAIService)
        {
            this._goalRepository = goalRepository;
            this._azureOpenAIService = openAIService;
        }

        public async Task<GoalDetail> GetGoalDetailById(Guid goalId)
        {
            return await _goalRepository.GetGoalDetailById(goalId);
        }

        public async Task<List<Goal>> GetMyGoals(Guid userId)
        {
            return await _goalRepository.GetMyGoals(userId).ContinueWith(t => t.Result.ToList());
        }

        public Task<RefinedGoalResponse> GetRefinedGoalsResponse(string userPrompt)
        {
            var request = new RefineGoalRequest
            {
                RawGoal = userPrompt
            };

            return this._azureOpenAIService.RefineRawGoal(request);
        }

        public async Task<bool> MarkGoalTaskAsCompleted(Guid goalTaskId)
        {
            return await _goalRepository.MarkGoalTaskAsCompleted(goalTaskId);
        }

        public async Task<GoalDetail> SaveGoalAndTasks(GoalDetail goalDetail)
        {
            return await _goalRepository.SaveGoalAndTasks(goalDetail);
        }
    }
}
