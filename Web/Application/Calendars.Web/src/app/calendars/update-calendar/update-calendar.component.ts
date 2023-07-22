import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Calendar } from 'src/app/shared/domain/calendar';
import { ModalService } from 'src/app/shared/services/modal.service';
import { CalendarsService } from '../services/calendars.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, map, switchMap, from, of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { IResponse } from 'src/app/shared/services/resources-http-client';

@Component({
  selector: 'app-update-calendar',
  templateUrl: './update-calendar.component.html',
  styleUrls: ['./update-calendar.component.css']
})
export class UpdateCalendarComponent implements OnInit {
  public name: FormControl = new FormControl('', 
  [
    Validators.required,
    Validators.minLength(1),
    Validators.maxLength(32),
  ]);
  public year: FormControl = new FormControl('',
  [
    Validators.required,
    Validators.min(0),
    Validators.max(3000)
  ]);
  public type: FormControl = new FormControl('', 
  [
    Validators.required,
  ]);
  public form: FormGroup = new FormGroup({
    name: this.name,
    year: this.year,
    type: this.type,
  });

  public calendar: Calendar | null = null;
  
  public updateCalendarErrorModalId: string 
  = "UpdateCalendarErrorModalId";
  public updateCalendarSuccessModalId: string 
    = "UpdateCalendarSuccessModalId";
  public updateCalendarCriticalErrorModalId: string 
    = "UpdateCalendarCriticalErrorModalId";

  public errors: string[] = [];
  public isLoading: boolean = true;
  public buttonIsDisabled: boolean = false;


  constructor(
    private modal: ModalService,
    private calendars: CalendarsService,
    private router: Router,
    private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.findCalendarById().subscribe({
      next: (response) => {
        this.calendar = response.result as Calendar;
        this.isLoading = false;

        this.updateFormValues();
      },
      error: this.createHandlerCriticalError()
    });
  }
  public update(): void {
    this.buttonIsDisabled = true;
    this.updateCalendar().subscribe({
      next: (response) => {
        this.modal.toggleModal(this.updateCalendarSuccessModalId);
        this.buttonIsDisabled = false;
      },
      error: (err) => {
        this.errors = err.error.messages;
        this.modal.toggleModal(this.updateCalendarErrorModalId);
        this.buttonIsDisabled = false;
      }
    })
  }

  public findCalendarById(): Observable<IResponse> {
    return this.route.params.pipe(
      map(p => p.id),
      switchMap(id => {
        return this.calendars.getById(id);
      })
    )
  }
  public updateCalendar(): Observable<IResponse> {
    return of(this.returnCalendarIfItIsNotNull()).pipe(
      switchMap((calendar) => {
        return this.calendars.updateByCalendarId(
          new Calendar(
            calendar.id,
            calendar.userId,
            this.name.value,
            this.year.value,
            this.type.value,
            calendar.days)) 
          })
      );
  }
  public returnCalendarIfItIsNotNull() {
    if (this.calendar === null) 
      throw new Error('Calendar cannot be a null value.');

    return this.calendar;
  }
  public createRedirectToCalendarsFunction(): () => void {
    return () => this.router.navigateByUrl('/calendars');
  }
  public updateFormValues() {
    this.name.setValue(this.calendar?.name);
    this.type.setValue(this.calendar?.type);
    this.year.setValue(this.calendar?.year);
  }

  private createHandlerCriticalError() {
    return (err: ErrorEvent) => {
      this.errors = err.error.messages;
      if (this.errors.values().next()) {
        this.errors = ['Try to reach external server is failed. Try again later!'];
      }
      this.modal.toggleModal(this.updateCalendarCriticalErrorModalId);
    };
  }
}
