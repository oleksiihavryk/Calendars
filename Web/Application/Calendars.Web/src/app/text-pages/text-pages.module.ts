import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TextPagesRoutingModule } from './text-pages-routing.module';

import { HomeComponent } from './home/home.component';
import { InfoComponent } from './info/info.component';


@NgModule({
  declarations: [
    HomeComponent,
    InfoComponent
  ],
  imports: [
    CommonModule,
    TextPagesRoutingModule
  ]
})
export class TextPagesModule { }
