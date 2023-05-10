import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';

import {AuthModule, LogLevel} from 'angular-auth-oidc-client';

import { environment } from 'src/environments/environment';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    LoginComponent,
    LogoutComponent,
    UnauthorizedComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    SharedModule,
    AuthModule.forRoot({
      config: {
        authority: environment.auth.authority,
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: environment.auth.clientId,
        scope: environment.auth.scope,
        responseType: 'code',
        logLevel: LogLevel.Debug,
      }
    })
  ]
})
export class AuthenticationModule { }
