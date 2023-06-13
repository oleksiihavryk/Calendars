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
  public isLoading: boolean = true;
  public updateCalendarErrorModalId: string 
    = "UpdateCalendarErrorModalId";
  public updateCalendarSuccessModalId: string 
    = "UpdateCalendarSuccessModalId";
  public errors: string[] = [];
  public get isUnlockToUpdate(): boolean {
    return this.calendar?.name !== this.name.value ||
      this.calendar?.type !== this.type.value ||
      this.calendar?.year !== this.year.value;
  }

  constructor(
    private modal: ModalService,
    private calendars: CalendarsService,
    private oidc: OidcSecurityService,
    private router: Router,
    private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.findCalendarById().subscribe({
      next: (response) => {
        this.calendar = response.result as Calendar;
        this.isLoading = false;

        this.updateFormValues();
      },
      error: (err) => {
        this.errors = err.error.messages;
        this.modal.toggleModal(this.updateCalendarErrorModalId);
        this.isLoading = false;
      },
    });
  }
  public update(): void {
    this.updateCalendar().subscribe({
      next: (response) => {
        this.modal.toggleModal(this.updateCalendarSuccessModalId);
      },
      error: (err) => {
        this.errors = err.error.messages;
        this.modal.toggleModal(this.updateCalendarErrorModalId);
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
  public createRedirectToCalendarFunction(): () => void {
    return () => this.router.navigateByUrl(
      '/calendar/'+this.calendar?.id);
  }
  public updateFormValues() {
    this.name.setValue(this.calendar?.name);
    this.type.setValue(this.calendar?.type);
    this.year.setValue(this.calendar?.year);
  }
}
