using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Domain.GoalTasks
{
    public class GoalTask
    {
        public Guid Id { get; set; }
        public Guid GoalId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
