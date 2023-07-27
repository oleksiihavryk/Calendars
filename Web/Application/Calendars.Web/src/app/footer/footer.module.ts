import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FooterRoutingModule } from './footer-routing.module';
import { FooterComponent } from './footer/footer.component';
import { ContactsComponent } from './contacts/contacts.component';
import { FooterNavigationComponent } from './footer-navigation/footer-navigation.component';


@NgModule({
  declarations: [
    FooterComponent,
    ContactsComponent,
    FooterNavigationComponent
  ],
  imports: [
    CommonModule,
    FooterRoutingModule
  ],
  exports: [
    FooterComponent
  ]
})
export class FooterModule { }
