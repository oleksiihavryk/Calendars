import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Calendar } from 'src/app/shared/domain/calendar';
import { ModalService } from 'src/app/shared/services/modal.service';
import { CalendarsService } from '../services/calendars.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, map, switchMap } from 'rxjs';
import { Router } from '@angular/router';
import { IResponse } from 'src/app/shared/services/resources-http-client';

@Component({
  selector: 'app-create-calendar',
  templateUrl: './create-calendar.component.html',
  styleUrls: ['./create-calendar.component.css']
})
export class CreateCalendarComponent {
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
  })
  public errors: string[] = [];
  public createButtonDisabled = false;
  public createCalendarErrorModalId = 
    'CreateCelendarErrorModalIdentifier';
  public createCalendarSuccessModalId = 
    'CreateCelendarSuccessModalIdentifier';

  constructor(
    private modal: ModalService,
    private calendars: CalendarsService,
    private oidc: OidcSecurityService,
    private router: Router) { }

  public create() {
    this.createButtonDisabled = true;
    this.createCalendar().subscribe({
      next: () => {
        this.modal.toggleModal(this.createCalendarSuccessModalId);
      },
      error: (response) => {
        this.createButtonDisabled = false;
        this.errors = response.error.messages;
        this.modal.toggleModal(this.createCalendarErrorModalId);
      },
      complete: () => {
        this.createButtonDisabled = false;
      }
    });
  }
  public createCalendar(): Observable<IResponse> {
    return this.oidc.getUserData().pipe(
      map(v => v.sub),
      switchMap(id => {
        const calendar = new Calendar(
          '00000000-0000-0000-0000-000000000000',
          id,
          this.name.value,
          this.year.value,
          this.type.value,
          []);
        return this.calendars.createNew(calendar);
      })
    );
  }
  public createRedirectToCalendarsFunction(): () => void {
    return () => {
      this.router.navigateByUrl('/calendars');
    };
  }
}
