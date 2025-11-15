export class UserLoginResponse
{
    isSuccess: boolean;
    accessToken: string;
    message: string;

    constructor(args: any){
        this.isSuccess = args.isSuccess;
        this.accessToken = args.accessToken;
        this.message = args.message;
    }
}