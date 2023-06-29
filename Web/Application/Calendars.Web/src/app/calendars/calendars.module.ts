import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CalendarsRoutingModule } from './calendars-routing.module';
import { CalendarsComponent } from './calendars/calendars.component';
import { SharedModule } from '../shared/shared.module';
import { CalendarTypePipe } from './pipes/calendar-type.pipe';
import { CalendarComponent } from './calendar/calendar.component';
import { MonthsNavigatorComponent } from './months-navigator/months-navigator.component';
import { NgxPageScrollCoreModule } from 'ngx-page-scroll-core';
import { CreateCalendarComponent } from './create-calendar/create-calendar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UpdateCalendarComponent } from './update-calendar/update-calendar.component';


@NgModule({
  declarations: [
    CalendarsComponent,
    CalendarTypePipe,
    CalendarComponent,
    MonthsNavigatorComponent,
    CreateCalendarComponent,
    UpdateCalendarComponent,
  ],
  imports: [
    CommonModule,
    CalendarsRoutingModule,
    NgxPageScrollCoreModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class CalendarsModule { }
