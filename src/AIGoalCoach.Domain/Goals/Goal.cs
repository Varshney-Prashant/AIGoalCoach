using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGoalCoach.Domain.Goals
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ActionableGoal { get; set; }
        public string RawInput { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
