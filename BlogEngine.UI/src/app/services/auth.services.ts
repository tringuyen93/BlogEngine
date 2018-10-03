import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ConfigurationService } from "./configuration.service";
import { EndpointFactorty } from "./endpoint-factory.service";
import { Subject } from "rxjs/Subject";
import { User } from "../models/user.model";
import { DBkeys } from "./dbkey";
import { LocalStoreManager } from "./local-store-manager";
import { LoginResponse, IdToken } from "../models/login-response.model";
import { map } from "rxjs/operators";
import { PermissionValues } from "../models/permission.model";
import { JwtHelper } from "./jwt-helper";

@Injectable()

export class AuthService {
    public loginRedirectUrl: string;
    public logoutRedirectUrl: string;
    private previousIsLoggedInCheck = false;
    private _loginStatus = new Subject<boolean>();
    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: EndpointFactorty, private localStorage: LocalStoreManager) {
        debugger;
        this.initializeLoginStatus();
    }

    private initializeLoginStatus() {
        this.localStorage.getInitEvent().subscribe(() => {
            this.reevaluateLoginStatus();
        });
    }

    login(userName: string, password: string, rememberMe?: boolean) {
        if (this.isLoggedIn)
            this.logout();

        return this.endpointFactory.getLoginEndpoint<LoginResponse>(userName, password).pipe(
            map(response => this.processLoginResponse(response, rememberMe)));
    }

    private processLoginResponse(response: LoginResponse, rememberMe: boolean) {

        let accessToken = response.access_token;

        if (accessToken == null)
            throw new Error("Received accessToken was empty");

        let idToken = response.id_token;
        let refreshToken = response.refresh_token || this.refreshToken;
        let expiresIn = response.expires_in;

        let tokenExpiryDate = new Date();
        tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);

        let accessTokenExpiry = tokenExpiryDate;

        let jwtHelper = new JwtHelper();
        let decodedIdToken = <IdToken>jwtHelper.decodeToken(response.id_token);

        let permissions: PermissionValues[] = Array.isArray(decodedIdToken.permission) ? decodedIdToken.permission : [decodedIdToken.permission];

        // if (!this.isLoggedIn)
        //   this.configurations.import(decodedIdToken.configuration);

        let user = new User(
            decodedIdToken.sub,
            decodedIdToken.name,
            decodedIdToken.fullname,
            decodedIdToken.email,
            decodedIdToken.jobtitle,
            decodedIdToken.phone,
            Array.isArray(decodedIdToken.role) ? decodedIdToken.role : [decodedIdToken.role]);
        user.isEnabled = true;

        this.saveUserDetails(user, permissions, accessToken, idToken, refreshToken, accessTokenExpiry, rememberMe);

        this.reevaluateLoginStatus(user);

        return user;
    }

    private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, idToken: string, refreshToken: string, expiresIn: Date, rememberMe: boolean) {
        if (rememberMe) {
            this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
            this.localStorage.savePermanentData(idToken, DBkeys.ID_TOKEN);
            this.localStorage.savePermanentData(refreshToken, DBkeys.REFRESH_TOKEN);
            this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
            this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
            this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);
        }
        else {
            this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
            this.localStorage.saveSyncedSessionData(idToken, DBkeys.ID_TOKEN);
            this.localStorage.saveSyncedSessionData(refreshToken, DBkeys.REFRESH_TOKEN);
            this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
            this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
            this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
        }
        this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
    }

    logout(): void {
        this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
        this.localStorage.deleteData(DBkeys.ID_TOKEN);
        this.localStorage.deleteData(DBkeys.REFRESH_TOKEN);
        this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
        this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
        this.localStorage.deleteData(DBkeys.CURRENT_USER);
        this.reevaluateLoginStatus();
    }

    private reevaluateLoginStatus(currentUser?: User) {
        let user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
        let isLoggedIn = user != null;

        if (this.previousIsLoggedInCheck != isLoggedIn) {
            setTimeout(() => {
                this._loginStatus.next(isLoggedIn);
            });
        }
        this.previousIsLoggedInCheck = isLoggedIn;
    }

    get currentUser(): User {
        let user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
        this.reevaluateLoginStatus(user);
        return user;
    }

    get isLoggedIn(): boolean {
        return this.currentUser != null;
    }

    get rememberMe(): boolean {
        return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) == true;
    }
    get refreshToken(): string {
        this.reevaluateLoginStatus();
        return this.localStorage.getData(DBkeys.REFRESH_TOKEN);
    }
}