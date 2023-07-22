import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidationResult } from 'angular-auth-oidc-client';
import { CalendarsService } from 'src/app/calendars/services/calendars.service';
import { DaysService } from 'src/app/days/services/days.service';
import { Calendar } from 'src/app/shared/domain/calendar';
import { Day } from 'src/app/shared/domain/day';
import { Event } from 'src/app/shared/domain/event';
import { ModalService } from 'src/app/shared/services/modal.service';
import { EventsService } from '../services/events.service';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
  private timeParser: RegExp = new RegExp('\\d+', 'g');

  public name: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(0),
    Validators.maxLength(32)
  ]);
  public timeFrom: FormControl = new FormControl('', [
    Validators.required,
    this.createTimeValidatorFrom()
  ]);
  public timeTo: FormControl = new FormControl('', [
    Validators.required,
    this.createTimeValidatorTo()
  ]);
  public description: FormControl = new FormControl('', [
    Validators.maxLength(128)
  ]);
  public form: FormGroup = new FormGroup({
    name: this.name,
    description: this.description,
    timeFrom: this.timeFrom,
    timeTo: this.timeTo
  });
  public calendar: Calendar | null = null;
  public day: Day | null = null;
  public addEventErrorModalId: string = 'AddEventErrorModalId';
  public addEventIsSuccessModalId: string = 'AddEventIsSuccessModalId';
  public errorMessages: string[] = [];
  public createButtonDisabled: boolean = false;
  public isWaiting: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private events: EventsService,
    private days: DaysService,
    private calendars: CalendarsService,
    private modal: ModalService) { }
  
  ngOnInit(): void {
    this.route.queryParams.subscribe((v) => {
      this.isWaiting = true;
      const id = v.id;

      this.days.getById(id).subscribe({
        next: (response) => {
          this.day = response.result as Day;
          this.calendars.getById(this.day.calendarId).subscribe({
            next: (response) => {
              this.calendar = response.result as Calendar;
              this.isWaiting = false;
            },
            error: this.handleError
          });
        },
        error: this.handleError
      });
    })
  }

  public create(): void {  
    if (this.form.invalid) 
      throw new Error('You cannot create a event if creating form is invalid!');
    
    this.createButtonDisabled = true;
    
    const [hoursFrom, minutesFrom] = (this.timeFrom?.value ?? '').
      matchAll(this.timeParser);
    const [hoursTo, minutesTo] = (this.timeTo?.value ?? '').
      matchAll(this.timeParser);
    const event = new Event(
      '00000000-0000-0000-0000-000000000000',
      this.day?.id ?? '',
      this.name.value,
      Number.parseInt(hoursFrom[0]),
      Number.parseInt(hoursTo[0]),
      Number.parseInt(minutesFrom[0]),
      Number.parseInt(minutesTo[0]),
      this.description.value);

    this.events.createNew(event).subscribe({
      next: (response) => {
        this.modal.toggleModal(this.addEventIsSuccessModalId);
        this.createButtonDisabled = false;
      },
      error: (err) => {
        this.createButtonDisabled = false;
        this.handleError(err);
      } 
    })
  }
  public createRedirectToDayAction(): () => void {
    return () => this.router.navigateByUrl(this.createUrlToDayNavigation());
  }
  public createUrlToDayNavigation(): string {
    const date = new Date(this.calendar?.year ?? 0, 0, this.day?.dayNumber)
    return `/calendar/${this.day?.calendarId}`+
      `/month/${date.getMonth()}`+
      `/day/${date.getDate()}`;
  }
  public handleError(err: ErrorEvent) {
    this.errorMessages = err.error.messages;
    this.modal.toggleModal(this.addEventErrorModalId);
  }

  private createTimeValidatorFrom(): (control: FormControl) => ValidationErrors | null {
    return (control) => {
      if (this !== undefined && this.timeFrom !== undefined && this.timeTo !== undefined) {
        const [hoursFrom, minutesFrom] = (this.timeFrom?.value ?? '').
          matchAll(this.timeParser);
        const [hoursTo, minutesTo] = (this.timeTo?.value ?? '').
          matchAll(this.timeParser);
  
        if (hoursFrom && minutesFrom && hoursTo && minutesTo) {
          const dateFrom = new Date(0);
          const dateTo = new Date(0); 
          
          dateFrom.setHours(Number.parseInt(hoursFrom[0]));
          dateFrom.setMinutes(Number.parseInt(minutesFrom[0]));
          dateTo.setHours(Number.parseInt(hoursTo[0]));
          dateTo.setMinutes(Number.parseInt(minutesTo[0]));
  
          if (dateFrom > dateTo) {
            return { 
              incorrectTimeSpanFrom: { 
                fromHours:hoursFrom[0],
                fromMin:minutesFrom[0],
                toHours:hoursTo[0],
                toMin:minutesTo[0],
              } 
            }
          } 

          return null;
        }
      }
  
      return { incorrectTimeSpanFrom: { } };
    }
  }
  private createTimeValidatorTo(): (control: FormControl) => ValidationErrors | null {
    return (control) => {
      if (this !== undefined && this.timeFrom !== undefined && this.timeTo !== undefined) {
        const [hoursFrom, minutesFrom] = (this.timeFrom?.value ?? '').
          matchAll(this.timeParser);
        const [hoursTo, minutesTo] = (this.timeTo?.value ?? '').
          matchAll(this.timeParser);
  
        if (hoursFrom && minutesFrom && hoursTo && minutesTo) {
          const dateFrom = new Date(0);
          const dateTo = new Date(0); 
          
          dateFrom.setHours(Number.parseInt(hoursFrom[0]));
          dateFrom.setMinutes(Number.parseInt(minutesFrom[0]));
          dateTo.setHours(Number.parseInt(hoursTo[0]));
          dateTo.setMinutes(Number.parseInt(minutesTo[0]));
  
          if (dateFrom > dateTo) {
            return {
              incorrectTimeSpanTo: { 
                fromHours:hoursFrom[0],
                fromMin:minutesFrom[0],
                toHours:hoursTo[0],
                toMin:minutesTo[0],
              }
            }
          }
          
          return null;
        }
      }
  
      return { incorrectTimeSpanTo: { } };
    }
  }
}
