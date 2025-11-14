using AIGoalCoach.Domain.Goals;
using AIGoalCoach.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AIGoalCoach.Repositories.GoalsRepository
{
    public class GoalRepository : IGoalRepository
    {
        public ApplicationDbContext _dbContext;

        public GoalRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<GoalDetail> GetGoalDetailById(Guid goalId)
        {
           return await _dbContext.Goals
            .Where(g => g.Id == goalId)
            .Select(g => new GoalDetail
            {
                Id = g.Id,
                UserId = g.UserId,
                ActionableGoal = g.ActionableGoal,
                RawInput = g.RawInput,
                CreatedOn = g.CreatedOn,
                GoalTasks = _dbContext.GoalTasks
                    .Where(t => t.GoalId == g.Id)
                    .ToList()
            })
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Goal>> GetMyGoals(Guid userId)
        {
            return await this._dbContext.Goals
                .Where(g => g.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> MarkGoalTaskAsCompleted(Guid goalTaskId)
        {
            var goalTask = await _dbContext.GoalTasks.FindAsync(goalTaskId);
            if (goalTask == null)
            {
                return false;
            }

            goalTask.IsCompleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<GoalDetail> SaveGoalAndTasks(GoalDetail goalDetail)
        {
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                UserId = goalDetail.UserId,
                ActionableGoal = goalDetail.ActionableGoal,
                RawInput = goalDetail.RawInput,
                CreatedOn = DateTime.UtcNow
            };
            await _dbContext.Goals.AddAsync(goal);
            foreach (var task in goalDetail.GoalTasks)
            {
                task.Id = Guid.NewGuid();
                task.GoalId = goal.Id;
                await _dbContext.GoalTasks.AddAsync(task);
            }
            await _dbContext.SaveChangesAsync();
            return await this.GetGoalDetailById(goal.Id);
        }
    }
}
