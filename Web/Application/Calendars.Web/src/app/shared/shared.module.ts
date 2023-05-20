import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { WaitingMarkComponent } from './waiting-mark/waiting-mark.component';

@NgModule({
  declarations: [
    ModalComponent,
    WaitingMarkComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ModalComponent,
    WaitingMarkComponent
  ]
})
export class SharedModule { }
