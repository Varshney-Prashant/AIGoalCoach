using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AIGoalCoach.Application.Configurations
{
    public static class IntentClassifier
    {
        private static readonly string[] GoalVerbs =
        {
            "increase","improve","reduce","grow","create","develop", "launch","build", "streamline", "average", 
            "optimize","achieve","deliver","launch","scale","build", "drive", "generate", "refactor", "reduce", "maximize", "minimize", "automate","enhance","complete", "expand","streamline", "boost", "strengthen", "elevate", "advance",
        };

        public static IntentType ClassifyIntent(this string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return IntentType.Unclear;

            userInput = userInput.Trim().ToLower();

            if (userInput.Length < 5 || userInput.Split(' ').Length < 2)
                return IntentType.Unclear;

            if (Regex.IsMatch(userInput, @"(i want to|i need to|my goal is|i aim to|i plan to|i would like to|)"))
                return IntentType.Goal;

            if (GoalVerbs.Any(v => userInput.Contains(v)))
                return IntentType.Goal;

            return IntentType.Unclear;
        }
    }

    public enum IntentType
    {
        Goal,
        Unclear
    }
}
