using AIGoalCoach.Domain.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Repositories.GoalsRepository
{
    public interface IGoalRepository
    {
        Task<IEnumerable<Goal>> GetMyGoals(Guid userId);
        Task<GoalDetail> GetGoalDetailById(Guid goalId);
        Task<GoalDetail> SaveGoalAndTasks(GoalDetail goalDetail);
        Task<bool> MarkGoalTaskAsCompleted(Guid goalTaskId);
    }
}
