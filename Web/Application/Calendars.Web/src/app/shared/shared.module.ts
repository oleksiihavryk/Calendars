import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { WaitingMarkComponent } from './waiting-mark/waiting-mark.component';
import { UpdateWindowComponent } from './update-window/update-window.component';
import { InputComponent } from './input/input.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent,
    InputComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent,
    InputComponent
  ]
})
export class SharedModule { }
