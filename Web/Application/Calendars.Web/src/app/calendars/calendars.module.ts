import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarsRoutingModule } from './calendars-routing.module';
import { CalendarsComponent } from './calendars/calendars.component';
import { SharedModule } from '../shared/shared.module';
import { CalendarTypePipe } from './pipes/calendar-type.pipe';
import { CalendarComponent } from './calendar/calendar.component';
import { MonthsNavigatorComponent } from './months-navigator/months-navigator.component';
import { NgxPageScrollCoreModule } from 'ngx-page-scroll-core';


@NgModule({
  declarations: [
    CalendarsComponent,
    CalendarTypePipe,
    CalendarComponent,
    MonthsNavigatorComponent
  ],
  imports: [
    CommonModule,
    CalendarsRoutingModule,
    NgxPageScrollCoreModule,
    SharedModule
  ]
})
export class CalendarsModule { }
