import { Injectable } from "@angular/core";
import { ConfigurationService } from "./configuration.service";
import { Observable } from "rxjs/Observable";
import { HttpHeaders, HttpParams, HttpClient } from "@angular/common/http";

@Injectable()
export class EndpointFactorty{
    private readonly _loginUrl: string = "/connect/token";
    private get loginUrl() { return this.configurations.baseUrl + this._loginUrl; }
    constructor(protected http: HttpClient, protected configurations: ConfigurationService){}

    getLoginEndpoint<T>(userName: string, password: string): Observable<T> {
        let header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });    
        let params = new HttpParams()
          .append('username', userName)
          .append('password', password)
          .append('grant_type', 'password')
          .append('scope', 'openid email phone profile offline_access roles');    
        let requestBody = params.toString();    
        console.log(this.loginUrl);
        return this.http.post<T>(this.loginUrl, requestBody, { headers: header });
      }
}