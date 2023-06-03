import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { WaitingMarkComponent } from './waiting-mark/waiting-mark.component';
import { UpdateWindowComponent } from './update-window/update-window.component';

@NgModule({
  declarations: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent
  ]
})
export class SharedModule { }
