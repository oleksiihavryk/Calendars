import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { SidePanelComponent } from './side-panel/side-panel.component';



@NgModule({
  declarations: [
    HeaderComponent,
    SidePanelComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class HeaderModule { }
