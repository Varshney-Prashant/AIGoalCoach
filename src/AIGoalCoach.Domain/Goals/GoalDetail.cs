using AIGoalCoach.Domain.GoalTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Domain.Goals
{
    public class GoalDetail : Goal
    {
        public List<GoalTask> GoalTasks { get; set; } = new List<GoalTask>();
    }
}
