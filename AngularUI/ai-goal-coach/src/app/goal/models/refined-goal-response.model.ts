export class RefinedGoalResponse
{
    actionableGoal: string;
    goalTasks: string[];
    isGoalRefined: boolean;
    message: string;

    constructor(args: any)
    {
        this.actionableGoal = args.actionableGoal;
        this.goalTasks = args.goalTasks;
        this.isGoalRefined = args.isGoalRefined;
        this.message = args.message;
    }
}