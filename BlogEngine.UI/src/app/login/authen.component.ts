import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.services";

@Component({
    selector: 'app-authen',
    templateUrl: './authen.Component.html',
    styleUrls: ['./authen.component.scss']
})
export class AuthenComponent implements OnInit{
    constructor(private authService: AuthService) { }
    ngOnInit() {
        debugger;
        this.authService.login("tringuyenh", "Tp200896@#",false).subscribe(user=>{
            console.log(user);
        },errr=>{
            console.log(errr);  
        })
    }
    ngOnDestroy(){
    }
}