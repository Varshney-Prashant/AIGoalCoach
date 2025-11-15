using AIGoalCoach.Application.Services.Goals;
using AIGoalCoach.Domain.Goals;
using AIGoalCoach.Domain.Goals.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIGoalCoach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            this._goalService = goalService;
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IEnumerable<Goal>> GetMyGoals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _goalService.GetMyGoals(Guid.Parse(userId));
        }

        [HttpGet("detail/{goalId}")]
        [Authorize]
        public async Task<GoalDetail> GetGoalDetailById(Guid goalId)
        {
            return await _goalService.GetGoalDetailById(goalId);
        }

        [HttpPost("save")]
        [Authorize]
        public async Task<GoalDetail> SaveGoalAndTasks([FromBody] GoalDetail goalDetail)
        {
            goalDetail.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _goalService.SaveGoalAndTasks(goalDetail);
        }

        [HttpPost("completeTask")]
        [Authorize]
        public async Task<bool> MarkGoalTaskAsCompleted([FromBody] CompleteGoalTaskRequest goalTaskRequest)
        {
            return await _goalService.MarkGoalTaskAsCompleted(goalTaskRequest.GoalTaskId);
        }

        [HttpPost("refineGoal")]
        [Authorize]
        public async Task<RefinedGoalResponse> GetRefinedGoalsResponse([FromBody] RefineGoalRequest refineGoalRequest)
        {
            var x =  await _goalService.GetRefinedGoalsResponse(refineGoalRequest.RawGoal);
            return x;
        }
    }
}
