using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Domain.Goals.Dtos
{
    public class RefinedGoalResponse
    {
        public string ActionableGoal { get; set; }
        public List<string> GoalTasks { get; set; }
    }
}
