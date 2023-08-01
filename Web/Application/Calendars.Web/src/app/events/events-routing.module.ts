import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateEventComponent } from './create-event/create-event.component';
import { AuthGuard } from '../authentication/authentication-routing.module';
import { UpdateEventComponent } from './update-event/update-event.component';

const routes: Routes = [
  {
    path: 'create/event',
    component: CreateEventComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'update/event/:id',
    component: UpdateEventComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class EventsRoutingModule { }
