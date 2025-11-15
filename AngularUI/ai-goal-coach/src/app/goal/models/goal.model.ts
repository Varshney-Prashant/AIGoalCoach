export class Goal
{
    id: string;
    userId: string;
    actionableGoal: string;
    rawInput: string;
    createdOn: Date;

    constructor(args: any) {
        this.id = args.id || '00000000-0000-0000-0000-000000000000';
        this.userId = args.userId || '00000000-0000-0000-0000-000000000000';
        this.actionableGoal = args.actionableGoal || '';
        this.rawInput = args.rawInput || '';
        this.createdOn = args.createdOn || new Date();
    }

}