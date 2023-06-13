import { Component, Input } from '@angular/core';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent {
  @Input() type: string = '';
  @Input() name: string = '';
  @Input() disabled: boolean = false; 
  @Input() control: FormControl = new FormControl();
}
