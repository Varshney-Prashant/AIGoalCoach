using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Domain.Goals
{
    public class CompleteGoalTaskRequest
    {
        public Guid GoalTaskId { get; set; }
        public bool MarkAsComplete { get; set; }
    }
}
