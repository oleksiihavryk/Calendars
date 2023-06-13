import { Component, OnInit } from '@angular/core';
import { Calendar } from 'src/app/shared/domain/calendar';
import { CalendarsService } from '../services/calendars.service';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-delete-calendar',
  templateUrl: './delete-calendar.component.html',
  styleUrls: ['./delete-calendar.component.css']
})
export class DeleteCalendarComponent implements OnInit {
  public calendar: Calendar | null = null;
  public errorMessages: string[] = [];
  public errorModalId: string = 'DeleteCalendarErrorModalId'; 

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private calendars: CalendarsService,
    private modal: ModalService) { }
  
  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(d => {
        return this.calendars.getById(d.id);
      })
    ).subscribe({
      next: (response) => {
        this.calendar = response.result as Calendar;
      },
      error: (e) => {
        this.errorMessages = e.error.messages;
        this.modal.toggleModal(this.errorModalId);
      }
    });
  }

  public delete(): void {
    if (this.calendar !== null) {
      this.calendars.delete(this.calendar).subscribe({
        next: () => {
          this.router.navigateByUrl('/calendars');
        },
        error: (e) => {
          this.errorMessages = e.error.messages;
          this.modal.toggleModal(this.errorModalId);    
        },
      });
    }
  }
  public createRedirectToCalendarPage(): () => void {
    return () => this.router.navigateByUrl('/calendar/'+this.calendar?.id);
  }

}
