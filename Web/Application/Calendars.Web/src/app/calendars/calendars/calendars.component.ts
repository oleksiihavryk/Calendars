import { Component, OnInit } from '@angular/core';
import { Calendar } from 'src/app/shared/domain/calendar';
import { CalendarsService } from '../services/calendars.service';

@Component({
  selector: 'app-calendars',
  templateUrl: './calendars.component.html',
  styleUrls: ['./calendars.component.css']
})
export class CalendarsComponent implements OnInit {
  public calendars: Calendar[] = [];
  public errorMessages: string[] = [];

  constructor(private calendarsService: CalendarsService) { }

  ngOnInit(): void {
    this.calendarsService.getAll().subscribe(response => {
      console.log(response);

      if (response.isSuccess) {
        this.calendars = response.result as Calendar[];
      } else {
        this.errorMessages = response.messages;
      }
    });
  }
}
