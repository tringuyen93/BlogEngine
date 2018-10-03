export class UserLogin {
    constructor(username?: string, password?: string, rememberMe?: boolean) {
        this.username = username;
        this.password = password;
        this.rememberMe = rememberMe;
    }

    username: string;
    password: string;
    rememberMe: boolean;
}