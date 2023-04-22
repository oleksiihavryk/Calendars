import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TextPagesRoutingModule } from './text-pages-routing.module';

import { HomeComponent } from './home/home.component';
import { InfoComponent } from './info/info.component';
import { SharedModule } from '../shared/shared.module';
import { NotFound404Component } from './not-found404/not-found404.component';


@NgModule({
  declarations: [
    HomeComponent,
    InfoComponent,
    NotFound404Component
  ],
  imports: [
    CommonModule,
    TextPagesRoutingModule,
    SharedModule
  ]
})
export class TextPagesModule { }
