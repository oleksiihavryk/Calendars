import { Component } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Event } from 'src/app/shared/domain/event';
import { EventsService } from '../services/events.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { DaysService } from 'src/app/days/services/days.service';
import { Day } from 'src/app/shared/domain/day';
import { CalendarsService } from 'src/app/calendars/services/calendars.service';
import { Calendar } from 'src/app/shared/domain/calendar';
import { map, switchMap } from 'rxjs';

@Component({
  selector: 'app-update-event',
  templateUrl: './update-event.component.html',
  styleUrls: ['./update-event.component.css']
})
export class UpdateEventComponent {
  private timeParser: RegExp = new RegExp('\\d+', 'g');

  public name: FormControl = new FormControl()
  public timeFrom: FormControl = new FormControl()
  public timeTo: FormControl = new FormControl()
  public description: FormControl = new FormControl();
  public form: FormGroup = new FormGroup({});

  public event: Event | undefined;

  public addEventErrorModalId: string = 'AddEventErrorModalId';
  public addEventIsSuccessModalId: string = 'AddEventIsSuccessModalId';
  public addEventCriticalErrorModalId: string = 'AddEventCriticalErrorModalId';

  public errorMessages: string[] = [];
  public isWaiting: boolean = false;
  public urlToDay: string = '';
  public updateButtonDisabled: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private events: EventsService,
    private days: DaysService,
    private calendars: CalendarsService,
    private modal: ModalService) { }
  
  ngOnInit(): void {
    this.route.params.subscribe((v) => {
      this.isWaiting = true;
      const id = v.id;
      let event: Event | undefined;
      let day: Day | undefined;
      let calendar: Calendar | undefined;

      const obs = this.events.getById(id).pipe(
        switchMap(r => {
          event = r.result as Event;
          return this.days.getById(event.dayId);
        }),
        switchMap(r => {
          day = r.result as Day;
          return this.calendars.getById(day.calendarId);
        }),
        map(r => {
          calendar = r.result as Calendar; 

          if (event === undefined || day === undefined || calendar === undefined)
            throw new Error('Event, day and calendar cannot be '+
              'undefined in current context');

          return {event, day, calendar};
        }),
      );
      obs.subscribe({
        next: (response) => {
          this.event = response.event;
          this.intitializeFormValuesByEvent(this.event);
          const date = new Date(response.calendar.year, 0, response.day.dayNumber);

          this.urlToDay = `/calendar/${response.day.calendarId}/`+
            `month/${date.getMonth()}/day/${date.getDate()}`;

          this.isWaiting = false;
        },
        error: (err) => {
          console.log(err);
          this.handleCriticalError(err);
        }
      })
    })
  }

  public intitializeFormValuesByEvent(e: Event) {
    const format = this.transfomNumberToDoubleDigitString;

    this.name = new FormControl(e.name, [
      Validators.required,
      Validators.minLength(0),
      Validators.maxLength(32)
    ]);
    this.timeFrom = new FormControl(format(e.hoursFrom)+':'+format(e.minutesFrom), [
      Validators.required,
      this.createTimeValidatorFrom()
    ]);
    this.timeTo = new FormControl(format(e.hoursTo)+':'+format(e.minutesTo), [
      Validators.required,
      this.createTimeValidatorTo()
    ]);
    this.description = new FormControl(e.description, [
      Validators.maxLength(128)
    ]);
    this.form = new FormGroup({
      name: this.name,
      description: this.description,
      timeFrom: this.timeFrom,
      timeTo: this.timeTo
    });
  }
  public update(): void {
    if (this.form.invalid) 
      throw new Error('You cannot create a event if creating form is invalid!');
    
    if (this.event === undefined) 
      throw new Error('You cannot update event because '+
        'event is not founded in system by id.')
    
    this.updateButtonDisabled = true;
    
    const [hoursFrom, minutesFrom] = (this.timeFrom?.value ?? '').
      matchAll(this.timeParser);
    const [hoursTo, minutesTo] = (this.timeTo?.value ?? '').
      matchAll(this.timeParser);
    const event = new Event(
      this.event.id,
      this.event.dayId,
      this.event.userId,
      this.name.value,
      Number.parseInt(hoursFrom[0]),
      Number.parseInt(hoursTo[0]),
      Number.parseInt(minutesFrom[0]),
      Number.parseInt(minutesTo[0]),
      this.description.value);

    this.events.updateByEventId(event).subscribe({
      next: () => {
        this.updateButtonDisabled = false;
        this.modal.toggleModal(this.addEventIsSuccessModalId);
      },
      error: (err) => {
        this.updateButtonDisabled = false;
        this.handleError(err)
      } 
    })
  }
  public createRedirectToDayAction(): () => void {
    return () => this.router.navigateByUrl(this.urlToDay);
  }
  public handleError(err: ErrorEvent) {
    this.errorMessages = err.error.messages;
    this.modal.toggleModal(this.addEventErrorModalId);
  }
  public handleCriticalError(err: ErrorEvent) {
    this.errorMessages = err.error.messages;
    this.modal.toggleModal(this.addEventCriticalErrorModalId);
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
  private transfomNumberToDoubleDigitString(num: number): string {
    if (num < 0) throw new Error('Number cannot be less than zero!');

    if (num < 10)  return '0'+num;
    else return num.toString();    
  } 
}
