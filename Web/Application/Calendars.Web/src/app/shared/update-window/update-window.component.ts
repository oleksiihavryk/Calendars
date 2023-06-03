import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-update-window',
  templateUrl: './update-window.component.html',
  styleUrls: ['./update-window.component.css']
})
export class UpdateWindowComponent {
  @Input() public isWorking: boolean = false;
}
