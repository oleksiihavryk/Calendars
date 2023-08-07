import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, switchMap } from 'rxjs';
import { CalendarsService } from 'src/app/calendars/services/calendars.service';
import { Calendar } from 'src/app/shared/domain/calendar';
import { Day, IArgbColor } from 'src/app/shared/domain/day';
import { ModalService } from 'src/app/shared/services/modal.service';
import { IResponse } from 'src/app/shared/services/resources-http-client';
import { DaysService } from '../services/days.service';
import $ from "jquery";
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Event } from 'src/app/shared/domain/event';
import { EventsService } from 'src/app/events/services/events.service';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css']
})
export class DayComponent implements OnInit {
  public date: Date = new Date();
  public calendar: Calendar | null = null;
  public day: Day | undefined;
  public deletedEvent: Event | undefined;

  public errorMessages: string[] = [];
  public successMessages: string[] = [];
  public isLoading: boolean = true;
  public createEventIsDisabled: boolean = false;

  public dayDefaultErrorModalId: string = 'DayErrorModalId';
  public daySuccessModalId: string = 'DaySuccessModalId';
  public dayCriticalErrorModalId: string = 'DayCriticalErrorModalId';
  public clearEventsModalId: string = 'ClearEventsModalId';
  public deleteEventModalId: string = 'DeleteEventModalId';
  public changeColorTabId: string = 'ChangeColorTab';

  public textColor: FormControl = new FormControl();
  public backgroundColor: FormControl = new FormControl();
  public changeColorForm: FormGroup = new FormGroup({});

  public get dayBackgroundColor(): string {
    const num = this.day?.backgroundArgbColorInteger;
    
    if (num === undefined)
      return '#ffffffff';

    const a = (num << 0) >>> 24,
            r = (num << 8) >>> 24,
            g = (num << 16) >>> 24, 
            b = (num << 24) >>> 24;
    const format = this.decimalNumToHexstring;

    return '#'+format(r)+format(g)+format(b)+format(a);
  }
  public get dayTextColor(): string {
    const num = this.day?.textArgbColorInteger;

    if (num === undefined)
      return '#000000FF';

    const a = (num << 0) >>> 24,
            r = (num << 8) >>> 24,
            g = (num << 16) >>> 24, 
            b = (num << 24) >>> 24;
    const format = this.decimalNumToHexstring;
    return '#'+format(r)+format(g)+format(b)+format(a);
  }

  constructor(
    private modal: ModalService,
    private calendars: CalendarsService,
    private days: DaysService,
    private events: EventsService,
    private route: ActivatedRoute,
    private router: Router,
    private authorize: AuthorizeService) { }
  
  ngOnInit(): void {
    this.findOrUpdateDayAndCalendar();
  }

  public findOrUpdateDayAndCalendar() {
    this.findCalendar().subscribe({
      next: (response) => {
        this.calendar = response.result as Calendar;
        
        this.route.params.subscribe(v => {
          const dayNumber = Number.parseInt(v.day);
          const month = Number.parseInt(v.month);
          
          this.date = new Date(this.calendar?.year ?? 0, month, dayNumber);
          
          if (this.checkCreatedDateByStartedValues(
            this.date,
            this.calendar?.year ?? 0,
            month,
            dayNumber)) { 
              this.day = this.findDay(this.calendar);
              this.initializeColorControls();
              this.isLoading = false;
            }
          })
        },
        error: this.errorHandler,
      });
  }
  public initializeColorControls(): void{
    const format = this.rgbaToRgb;
    this.textColor = new FormControl(format(this.dayTextColor), [
      Validators.required
    ]);
    this.backgroundColor = new FormControl(format(this.dayBackgroundColor), [
      Validators.required
    ]);  
    this.changeColorForm = new FormGroup({
      textColor: this.textColor,
      backgroundColor: this.backgroundColor
    });
  }
  public openChangeColorTab() {
    $('#'+this.changeColorTabId).slideToggle(200);
  }
  public checkCreatedDateByStartedValues(
    date: Date,
    year: number,
    month: number,
    day: number): boolean {
    if (date.getFullYear() !== year || 
      date.getMonth() !== month || 
      date.getDate() !== day) {
      this.isLoading = false;
      this.errorMessages = [
        "Url is broken.",
        "Incorrect month number or day number in url."];
      this.modal.toggleModal(this.dayCriticalErrorModalId);
      
      return false;
    }

    return true;
  }
  public changeColors() {
    const userId = this.authorize.userData.id;

    if (userId === undefined) 
      throw new Error('User id is not found! Is it happened probably because you are not logged in and trying to change colors.');

    if (this.changeColorForm.valid) {
      const format = this.hexStringToDecimalNum;
      const textColor = {
        a: 255,
        r: format(this.textColor.value.substring(1, 3)),
        g: format(this.textColor.value.substring(3, 5)),
        b: format(this.textColor.value.substring(5, 7))
      };
      const backgroundColor = {
        a: 255,
        r: format(this.backgroundColor.value.substring(1, 3)),
        g: format(this.backgroundColor.value.substring(3, 5)),
        b: format(this.backgroundColor.value.substring(5, 7)) 
      };

      if (this.day === undefined) {
        const obs = this.createNewDay(
          textColor, backgroundColor
        );
        obs.subscribe({
          next: (response) => {
            this.day = response.result as Day;
          },
          error: this.errorHandler
        });
      } else {
        const updateDayModel = new Day(
          this.day.id,
          this.day.calendarId,
          userId,
          this.day.dayNumber,
          0,
          0,
          this.day.events
        );

        updateDayModel.backgroundArgbColor = backgroundColor;
        updateDayModel.textArgbColor = textColor;

        const obs = this.days.updateByDayId(updateDayModel);
        obs.subscribe({
          next: (response) => {
            this.day = response.result as Day;
            this.successMessages = ["Colors is successfully configured " +
              "for this day!"];
            this.modal.toggleModal(this.daySuccessModalId);
          },
          error: this.errorHandler
        })
      }
    }
  }
  public clearEvents() {
    this.modal.toggleModal(this.clearEventsModalId);
  }
  public deleteEvent(e: Event) {
    this.deletedEvent = e;
    this.modal.toggleModal(this.deleteEventModalId);
  }
  public createClearEventsHandler(): () => void {
    return () => {
      const day = this.day ?? new Day('', '', '', 0, 0, 0, []);
      
      if (day.events.length === 0)
      {
        this.errorMessages = ["There is no any events at this day!"];
        this.modal.toggleModal(this.dayDefaultErrorModalId);
        return;
      }
      
      const obs = this.days.delete(day);
      obs.subscribe({
        next: (response) => {
          this.findOrUpdateDayAndCalendar();
          this.successMessages = response.messages;
          this.modal.toggleModal(this.daySuccessModalId);
        },
        error: this.errorHandler
      });
    };
  }
  public createDeleteEventHandler(): () => void {
    return () => {
      if (this.deletedEvent === undefined || this.day === undefined) {
        throw new Error('Deleted event or day cannot be undefined' + 
          'on this stage of deletion');
      }

      const obs = this.day.events.length > 1 ? this.events.delete(this.deletedEvent) : this.days.delete(this.day);
      obs.subscribe({
        next: () => {
          this.findOrUpdateDayAndCalendar();
          this.successMessages = [
            'Event is succesfully deleted!',
            'Wait for updating of events list.'
            ];
          this.modal.toggleModal(this.daySuccessModalId);
        },
        error: this.errorHandler
      })
    };
  }
  public addNewEvent(): void {
    this.createEventIsDisabled = true;

    if (this.day === undefined) {
      const obs = this.createNewDay();
      const sub = obs.subscribe({
        next: (response) => {
          this.day = response.result as Day;
          this.router.navigateByUrl('/create/event?id='+this.day.id);
          sub.unsubscribe();
        },
        error: (err) => {
          this.errorHandler(err);
          this.createEventIsDisabled = false;
          sub.unsubscribe();
        } 
      });

      return;
    }

    this.router.navigateByUrl('/create/event?id='+this.day.id);
  }
  public createNewDay(
    textColor: IArgbColor | undefined = undefined,
    backgroundColor: IArgbColor | undefined = undefined): Observable<IResponse> {
    const userId = this.authorize.userData.id;

    if (userId === undefined) 
      throw new Error('User id is not found! Is it happened probably because you are not logged in and trying to create day.');

    const yearFirstDay = Math.floor(
      new Date(this.date.getFullYear(), 0, 1).getTime() / 86400000);
    const dateDay = Math.ceil(this.date.getTime() / 86400000);
    const dayOfYear = dateDay - yearFirstDay; 
    
    const newDay = new Day(
      '00000000-0000-0000-0000-000000000000',
      this.calendar?.id ?? '',
      userId,
      dayOfYear,
      0,
      0,
      []
    );

    newDay.backgroundArgbColor = backgroundColor ?? { 
      a: 255,
      r: 255,
      g: 255,
      b: 255
    };
    newDay.textArgbColor = textColor ?? { 
      a: 255,
      r: 0,
      g: 0,
      b: 0
    }

    return this.days.createNew(newDay);
  }
  public findCalendar(): Observable<IResponse> {
    return this.route.params.pipe(
      map(v => {
        return v.id;
      }),
      switchMap(id => {
        return this.calendars.getById(id);
      })
    )
  }
  public findDay(calendar: Calendar | null): Day | undefined {
    return calendar?.days.find(
      (v) => {
        const d1 = new Date(calendar.year, 0, v.dayNumber);
        return d1.toUTCString() === this.date.toUTCString();
      });
  }
  public navigateToCalendar(): () => void {
    return () => {
      const obs = this.route.params.pipe(
        map(v => {
          return v.id;
        }),
        map(id => {
          this.router.navigateByUrl('/calendar/'+id);
        })
      );
      obs.subscribe(console.log);
    }
  }
  public errorHandler(err: ErrorEvent) {
    this.errorMessages = err.error.messages;
    this.modal.toggleModal(this.dayDefaultErrorModalId);    
  }
  public criticalErrorHandler(err: ErrorEvent) {
    this.errorMessages = err.error.messages;
    this.modal.toggleModal(this.dayCriticalErrorModalId);    
  }

  private decimalNumToHexstring(num: number): string {
    let result = num <= 16 ? '0' : '';
    result += num.toString(16);
    return result;
  } 
  private hexStringToDecimalNum(hex: string): number {
    return Number.parseInt(hex, 16);
  } 
  private rgbaToRgb(rgba: string): string {
    return rgba.substring(0, 7);
  }
}