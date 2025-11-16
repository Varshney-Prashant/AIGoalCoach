using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGoalCoach.Domain.Goals;
using AIGoalCoach.Domain.Goals.Dtos;

namespace AIGoalCoach.Application.Services.Goals
{
    public interface IGoalService
    {
        Task<List<Goal>> GetMyGoals(Guid userId);
        Task<GoalDetail> GetGoalDetailById(Guid goalId);
        Task<GoalDetail> SaveGoalAndTasks(Domain.Goals.GoalDetail goalDetail);
        Task<bool> MarkGoalTaskAsCompleted(CompleteGoalTaskRequest goalTaskRequest);
        Task<RefinedGoalResponse> GetRefinedGoalsResponse(string userPrompt);
    }
}
