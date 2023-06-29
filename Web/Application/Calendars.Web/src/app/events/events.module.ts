import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventsRoutingModule } from './events-routing.module';
import { CreateEventComponent } from './create-event/create-event.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { UpdateEventComponent } from './update-event/update-event.component';


@NgModule({
  declarations: [
    CreateEventComponent,
    UpdateEventComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    EventsRoutingModule,
    ReactiveFormsModule
  ]
})
export class EventsModule { }
