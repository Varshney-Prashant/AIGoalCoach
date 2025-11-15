export class UserLoginRequest
{
    emailAddress: string
    password: string

    constructor(args: any) {
        this.emailAddress = args.emailAddress;
        this.password = args.password;
    };
}