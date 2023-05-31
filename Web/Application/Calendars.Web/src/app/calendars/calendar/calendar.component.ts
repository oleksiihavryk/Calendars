import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, mergeMap } from 'rxjs';
import { Calendar } from 'src/app/shared/domain/calendar';
import { CalendarsService } from '../services/calendars.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { IResponse } from 'src/app/shared/services/resources-http-client';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  public notFoundModalId = 'CalendarNotFoundModalId';
  public calendar: Calendar = new Calendar('','','',0,0,[])
  public errorMessages: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private calendars: CalendarsService,
    private modal: ModalService) {}

  ngOnInit(): void {
    this.findOrUpdateCalendar(); 
  }

  public findOrUpdateCalendar(): Observable<IResponse> {
    const obs = this.route.params.pipe(
      map(v => {
        return v['id'];
      }),
      mergeMap((id) => {
        return this.calendars.getById(id);
      })
    );

    obs.subscribe({
      next: (response) => {
        this.calendar = response.result as Calendar;
        
        console.log(this.calendar);
      },
      error: (e) => {
        this.errorMessages = e.error.messages;
        this.calendar = e.error.result;
        this.modal.toggleModal(this.notFoundModalId);
      }
    })

    return obs;
  }
}
