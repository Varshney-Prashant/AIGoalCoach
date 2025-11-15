export class GoalTask
{
    id: string;
    goalId: string;
    description: string;
    isCopmpleted: boolean;
    createdOn: Date;

    constructor(args: any)
    {
        this.id = args.id || "00000000-0000-0000-0000-000000000000";
        this.goalId = args.goalId || '00000000-0000-0000-0000-000000000000';
        this.description = args.description || '';
        this.isCopmpleted = args.isCompleted || false;
        this.createdOn = args.createdOn || new Date();
    }
}