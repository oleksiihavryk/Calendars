import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarsRoutingModule } from './calendars-routing.module';
import { CalendarsComponent } from './calendars/calendars.component';
import { SharedModule } from '../shared/shared.module';
import { CalendarTypePipe } from './pipes/calendar-type.pipe';
import { CalendarComponent } from './calendar/calendar.component';


@NgModule({
  declarations: [
    CalendarsComponent,
    CalendarTypePipe,
    CalendarComponent
  ],
  imports: [
    CommonModule,
    CalendarsRoutingModule,
    SharedModule
  ]
})
export class CalendarsModule { }
