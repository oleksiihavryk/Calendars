import { NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { HeaderModule } from './header/header.module';
import { TextPagesModule } from './text-pages/text-pages.module';
import { FooterModule } from './footer/footer.module';
import { ProfileModule } from './profile/profile.module';
import { CalendarsModule } from './calendars/calendars.module';
import { DaysModule } from './days/days.module';
import { EventsModule } from './events/events.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HeaderModule,
    FooterModule,
    ProfileModule,
    CalendarsModule,
    DaysModule,
    EventsModule,
    TextPagesModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
