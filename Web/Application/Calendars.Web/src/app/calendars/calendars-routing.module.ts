import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarsComponent } from './calendars/calendars.component';
import { AuthGuard } from '../authentication/authentication-routing.module';

const routes: Routes = [
  {
    path: 'calendars',
    component: CalendarsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class CalendarsRoutingModule { }
