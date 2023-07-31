import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { AuthenticationModule } from '../authentication/authentication.module';
import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './profile/profile.component';
import { EmailComponent } from './email/email.component';
import { PasswordComponent } from './password/password.component';
import { UsernameComponent } from './username/username.component';
import { CalendarsInformationComponent } from './calendars-information/calendars-information.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileInputComponent } from './profile-input/profile-input.component';


@NgModule({
  declarations: [
    ProfileComponent,
    EmailComponent,
    PasswordComponent,
    UsernameComponent,
    CalendarsInformationComponent,
    ProfileInputComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ProfileRoutingModule,
    AuthenticationModule,
    SharedModule
  ]
})
export class ProfileModule { }
