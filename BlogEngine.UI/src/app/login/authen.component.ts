import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.services";
import { UserLogin } from "../models/user-login.model";

@Component({
    selector: 'app-authen',
    templateUrl: './authen.Component.html',
    styleUrls: ['./authen.component.scss']
})
export class AuthenComponent implements OnInit{
    private user = new UserLogin();
    constructor(private authService: AuthService) { }
    ngOnInit() {
        debugger;
    }
    login(){        
        this.authService.login(this.user.username, this.user.password, this.user.rememberMe).subscribe(user=>{
            console.log(user);
        },errr=>{
            console.log(errr);  
        })
    }
    ngOnDestroy(){
    }
}