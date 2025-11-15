export class UserRegisterRequest
{
    emailAddress: string;
    userName: string;
    password: string;

    constructor(args: any) {
        this.emailAddress = args.emailAddress;
        this.password = args.password;
        this.userName = args.userName;        
    }
}