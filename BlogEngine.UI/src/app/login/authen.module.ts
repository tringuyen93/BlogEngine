import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { MatButtonModule, MatRippleModule, MatInputModule, MatTooltipModule } from "@angular/material";
import { AuthenRoutes } from "./authen.routing";
import { AuthService } from "../services/auth.services";
import { EndpointFactorty } from "../services/endpoint-factory.service";
import { LocalStoreManager } from "../services/local-store-manager";
import { ConfigurationService } from "../services/configuration.service";
import { HttpModule } from "@angular/http";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AuthenRoutes),
    FormsModule,
    MatButtonModule,
    MatRippleModule,
    MatInputModule,
    MatTooltipModule
  ],
  declarations: [
  ],
  providers: [
    AuthService,
    EndpointFactorty,
    LocalStoreManager,
    ConfigurationService
  ]
})

export class AuthModule { }