import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from './modal/modal.component';
import { WaitingMarkComponent } from './waiting-mark/waiting-mark.component';
import { UpdateWindowComponent } from './update-window/update-window.component';
import { InputComponent } from './input/input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SafeStylePipe } from './pipes/safe-style.pipe';

@NgModule({
  declarations: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent,
    InputComponent,
    SafeStylePipe,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    ModalComponent,
    WaitingMarkComponent,
    UpdateWindowComponent,
    InputComponent,
    SafeStylePipe
  ]
})
export class SharedModule { }
