import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TextPagesRoutingModule } from './text-pages-routing.module';

import { HomeComponent } from './home/home.component';
import { InfoComponent } from './info/info.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    HomeComponent,
    InfoComponent
  ],
  imports: [
    CommonModule,
    TextPagesRoutingModule,
    SharedModule
  ]
})
export class TextPagesModule { }
