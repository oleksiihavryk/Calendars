import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-calendars-information',
  templateUrl: './calendars-information.component.html',
  styleUrls: ['./calendars-information.component.css']
})
export class CalendarsInformationComponent {
  @Input() calendarsCount: number = 0;
  @Input() eventsCount: number = 0;
}
