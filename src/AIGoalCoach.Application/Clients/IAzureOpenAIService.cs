using AIGoalCoach.Domain.Goals.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Application.Clients
{
    public interface IAzureOpenAIService
    {
        Task<RefinedGoalResponse> RefineRawGoal(RefineGoalRequest request);
    }
}
