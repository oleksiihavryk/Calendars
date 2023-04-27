import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {AuthModule} from 'angular-auth-oidc-client';

import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthModule.forRoot({
      config: {
        authority: environment.auth.authority,
        redirectUrl: window.location.origin,
        clientId: environment.auth.clientId,
        scope: environment.auth.scope,
        responseType: 'code',
        useRefreshToken: true, 
      }
    })
  ]
})
export class AuthenticationModule { }
