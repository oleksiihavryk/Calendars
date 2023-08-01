import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarsComponent } from './calendars/calendars.component';
import { AuthGuard } from '../authentication/authentication-routing.module';
import { CalendarComponent } from './calendar/calendar.component';
import { CreateCalendarComponent } from './create-calendar/create-calendar.component';
import { UpdateCalendarComponent } from './update-calendar/update-calendar.component';
import { DayComponent } from '../days/day/day.component';

const routes: Routes = [
  {
    path: 'calendars',
    component: CalendarsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'calendar',
    pathMatch: 'prefix',
    children: [
      {
        path: ':id/month/:month/day/:day',
        component: DayComponent,
        canActivate: [AuthGuard],
      },
      {
        path: ':id',
        component: CalendarComponent,
        canActivate: [AuthGuard],
      },
    ]
  },
  {
    path: 'update/calendar/:id',
    component: UpdateCalendarComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'create/calendar',
    component: CreateCalendarComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class CalendarsRoutingModule { }
