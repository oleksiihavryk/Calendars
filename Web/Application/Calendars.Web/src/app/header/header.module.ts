import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { SidePanelComponent } from './side-panel/side-panel.component';
import { HeaderRoutingModule } from './header-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuthenticationModule } from '../authentication/authentication.module';



@NgModule({
  declarations: [
    HeaderComponent,
    SidePanelComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    HeaderRoutingModule,
    AuthenticationModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class HeaderModule { }
