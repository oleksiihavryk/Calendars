import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-profile-input',
  templateUrl: './profile-input.component.html',
  styleUrls: ['./profile-input.component.css']
})
export class ProfileInputComponent {
  @Input() isChangesOpen: boolean = false;
  @Input() input: FormControl = new FormControl();
  @Input() valueIsNotChanges: boolean = false;
}
