import { GoalTask } from "./goal-task.model";
import { Goal } from "./goal.model";

export class GoalDetail extends Goal
{
    goalTasks: GoalTask[];

    constructor(args: any){
        super(args);
        this.goalTasks = args.goalTasks || [];
    }
}