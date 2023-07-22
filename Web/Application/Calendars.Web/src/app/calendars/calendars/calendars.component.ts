import { Component, OnInit } from '@angular/core';
import { CalendarsService } from '../services/calendars.service';
import { Calendar } from 'src/app/shared/domain/calendar';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { CalendarsSortingService, SortDirection } from '../services/calendars-sorting.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { Observable, from, map, mergeMap, switchMap } from 'rxjs';
import { IResponse } from 'src/app/shared/services/resources-http-client';

@Component({
  selector: 'app-calendars',
  templateUrl: './calendars.component.html',
  styleUrls: ['./calendars.component.css']
})
export class CalendarsComponent implements OnInit {
  public calendars: Calendar[] = [];
  public deletedCalendar: Calendar | undefined;
  
  public calendarsErrorModalId: string = 'CalendarsErrorModalId';
  public deleteCalendarModalId: string = 'DeleteCalendarModalId';
  public calendarsSuccessModalId: string = 'CalendarsSuccessModalId';
  
  public errorMessages: string[] = [];
  public successMessages: string[] = [];
  public firstUpdateRequest = true;
  public calendarsEmpty: boolean = true;
  public isUpdating: boolean = false;

  constructor(
    public calendarsSorting: CalendarsSortingService,
    public modal: ModalService,
    private router: Router,
    private route: ActivatedRoute,
    private calendarsService: CalendarsService) { }
  
  ngOnInit(): void {
    this.update();
    this.sortByRoute();
  }

  public deleteCalendar($event: MouseEvent, calendar: Calendar) {
    $event.stopPropagation();
    this.deletedCalendar = calendar;
    this.modal.toggleModal(this.deleteCalendarModalId);
    return false;
  }
  public createDeleteCalendarHandler(): () => void {
    return () => {
      const calendar = this.deletedCalendar;

      if (calendar === undefined) 
        throw new Error('Deleted calendar cannot be a '+
          'undefined on this stage of deleting.');
      
      const obs = this.calendarsService.delete(calendar);
      obs.subscribe({
        next: (response) => {
          this.update();
          this.successMessages = response.messages;
          this.modal.toggleModal(this.calendarsSuccessModalId);
          this.deletedCalendar = undefined;
        },
        error: (err) => {
          this.errorMessages = err.error.messages;
          this.modal.toggleModal(this.calendarsErrorModalId);
          this.deletedCalendar = undefined;
        }
      })
    };
  }

  public nameSortedDown() {
    return this.calendarsSorting.nameSortState === SortDirection.Down;
  }
  public nameSortedUp() {
    return this.calendarsSorting.nameSortState === SortDirection.Up;
  }
  public nameNotSorted() {
    return this.calendarsSorting.nameSortState === SortDirection.None;
  }
  public yearSortedDown() {
    return this.calendarsSorting.yearSortState === SortDirection.Down;
  }
  public yearSortedUp() {
    return this.calendarsSorting.yearSortState === SortDirection.Up;
  }
  public yearNotSorted() {
    return this.calendarsSorting.yearSortState === SortDirection.None;
  }
  public typeSortedDown() {
    return this.calendarsSorting.typeSortState === SortDirection.Down;
  }
  public typeSortedUp() {
    return this.calendarsSorting.typeSortState === SortDirection.Up;
  }
  public typeNotSorted() {
    return this.calendarsSorting.typeSortState === SortDirection.None;
  }
  public sortTypeUp() {
    this.router.navigateByUrl('/calendars?sort=type&type=up');
    this.calendarsSorting.sortTypeUp(this.calendars);
  }
  public sortTypeDown() {
    this.router.navigateByUrl('/calendars?sort=type&type=down');
    this.calendarsSorting.sortTypeDown(this.calendars);
  }
  public sortNameUp() {
    this.router.navigateByUrl('/calendars?sort=name&type=up');
    this.calendarsSorting.sortNameUp(this.calendars);
  }
  public sortNameDown() {
    this.router.navigateByUrl('/calendars?sort=name&type=down');
    this.calendarsSorting.sortNameDown(this.calendars);
  }
  public sortYearUp() {
    this.router.navigateByUrl('/calendars?sort=year&type=up');
    this.calendarsSorting.sortYearUp(this.calendars);
  }
  public sortYearDown() {
    this.router.navigateByUrl('/calendars?sort=year&type=down');
    this.calendarsSorting.sortYearDown(this.calendars);
  }

  public update() : Observable<undefined> {
    this.isUpdating = true;
    const req = this.calendarsService.getAll().pipe(
      map((response: IResponse) => {
        this.calendars = response.result as Calendar[];
        this.calendarsEmpty = false;

        this.isUpdating = false;

        return undefined;
      })
    );
    req.subscribe({
      next: () => {
        if (this.firstUpdateRequest) {
          this.firstUpdateRequest = false;
          return;
        }

        this.router.navigateByUrl('/calendars');
        this.calendarsSorting.removeSorting();
      },
      error: (err) => {
        if (err.status === 404 && err.error != undefined) {
          this.calendars = err.error.result as Calendar[];
          this.calendarsEmpty = true;
        } else {
          this.calendars = err.error.result as Calendar[];
          this.errorMessages = err.error.messages;
          this.modal.toggleModal(this.calendarsErrorModalId);
        }

        this.isUpdating = false;

        if (this.firstUpdateRequest) {
          this.firstUpdateRequest = false;
          return;
        }

        this.router.navigateByUrl('/calendars');
        this.calendarsSorting.removeSorting();
      },
    });
    return req;
  }
  public sortByRoute() {
    this.route.queryParams.subscribe(params => {
      const sort = params['sort'];
      const type = params['type'];

      if (sort === undefined) return;

      switch (sort) {
        case 'name': {
          if (type === 'up') {
            this.sortNameUp();
          } else {
            this.sortNameDown();
          }
          break;
        }
        case 'type': {
          if (type === 'up') {
            this.sortTypeUp();
          } else {
            this.sortTypeDown();
          }
          break;
        }
        case 'year': {
          if (type === 'up') {
            this.sortYearUp();
          } else {
            this.sortYearDown();
          }
          break;
        }
        default: break;
      }
    })
  }

  public goToCalendar(c: Calendar) {
    this.router.navigateByUrl(`/calendar/${c.id}`);
  }
}
