import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarsRoutingModule } from './calendars-routing.module';
import { CalendarsComponent } from './calendars/calendars.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    CalendarsComponent
  ],
  imports: [
    CommonModule,
    CalendarsRoutingModule,
    SharedModule
  ]
})
export class CalendarsModule { }
