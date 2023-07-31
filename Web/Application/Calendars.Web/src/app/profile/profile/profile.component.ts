import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';
import { CalendarsService } from 'src/app/calendars/services/calendars.service';
import { Calendar } from 'src/app/shared/domain/calendar';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public isUpdating: boolean = false;
  public calendarsCount: number = 0;
  public eventsCount: number = 0;
  public errorMessages: string[] = [];

  public profileErrorModalId: string = 'ProfileErrorModalId';
  public featureIsUnavailableModalId: string = 'FeatureIsUnavailableModalId';

  public featureName: string = '';

  constructor(
    public auth: AuthorizeService,
    private modal: ModalService,
    private calendars: CalendarsService) { }

  ngOnInit(): void {
    this.isUpdating = true;
    this.calendars.getAll().subscribe({
      next: (response) => {
        const calendars = response.result as Calendar[];
        let eventsCount = 0;
        
        calendars.forEach(c => c.days.forEach(d => eventsCount += d.events.length));
        
        this.calendarsCount = calendars.length;
        this.eventsCount = eventsCount;
        this.isUpdating = false;
      },
      error: (err) => {
        if (err.error.statusCode === 404) {
          this.calendarsCount = 0;
          this.eventsCount = 0;
          this.isUpdating = false;
        } else {
          this.errorHandler(err);
        }
      }
    });
  }

  private errorHandler(err: ErrorEvent): void {
      this.errorMessages = err.error.messages;
      if (err.error.messages === undefined || err.error.messages.lenght === 0) {
        this.errorMessages = ['Unknown error on server side. Try again later!'];
      }
      this.modal.toggleModal(this.profileErrorModalId);
  }
}
