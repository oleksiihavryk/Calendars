import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  public calendar: Calendar | null = null;
  public errorMessages: string[] = [];
  public showLoaderScrean: boolean = false;
  public months: (Date | null)[][][] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private calendars: CalendarsService,
    private modal: ModalService) {}

  ngOnInit(): void {
    this.findOrUpdateCalendar().subscribe((response) => {
      const result = response.result as Calendar;
      const yearByDays = this.assembleYearByDays(result?.year ?? 0);
      
      this.months = this.createMonthsByYear(yearByDays);

      this.showLoaderScrean = false;
    }); 
  }

  public findOrUpdateCalendar(): Observable<IResponse> {
    this.showLoaderScrean = true;
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
      },
      error: (e) => {
        this.errorMessages = e.error.messages;
        this.calendar = e.error.result;
        this.modal.toggleModal(this.notFoundModalId);
      },
    })

    return obs;
  }
  public assembleYearByDays(year: number): Date[] {
    const yearByDays: Date[] = [];
    const isIntercalary = new Date(year, 0, 365).getDate() === 30;
    const days: number = isIntercalary ? 366 : 365;

    let i = 0;
    while (i++ < days) 
      yearByDays.push(new Date(year, 0, i));

    return yearByDays;
  }
  public createMonthsByYear(year: Date[]) {
    const weeksCount = Math.round(year.length / 7);
    let weeks = [];
    let spanIndex = year[0].getDay();

    const firstWeek: (Date | null)[] = [];

    for (let j = 0; j < spanIndex; j++) {
      firstWeek.push(null);
    }

    year.slice(0, 7 - spanIndex).map(d => firstWeek.push(d));
    weeks.push(firstWeek);

    for(let i = 0; i < weeksCount; i++) {
      let index = i * 7 + spanIndex - 1;
      weeks.push(year.slice(index, index + 7));
    }

    for (let j = 0; j < 7 - weeks[weeksCount].length; j++) {
      weeks[weeksCount].push(null);
    }

    let months: (Date | null)[][][] = [];
    let monthsCount = 12;
    let firstWeekOfMonth = 0;

    while (monthsCount --> 0) {
      const week = weeks[firstWeekOfMonth];
      const weeksSpanIndex = this.calculateWeeksInMonthsByWeek(week)
      const firstWeekOfNextMonth = firstWeekOfMonth + weeksSpanIndex;
      const weeksOfMonths = weeks
        .slice(firstWeekOfMonth, firstWeekOfNextMonth);
      months.push(weeksOfMonths);
      firstWeekOfMonth = firstWeekOfNextMonth;
    }

    return months;
  }
  public createRedirectToCalendarsFunction(): () => void {
    return () => {
      this.router.navigateByUrl('/calendars');
    };
  }
  public isBeginOfMonth(week: (Date | null)[]) : boolean {
    const notNullDays = week.filter(d => d !== null);
    const lastDayOfWeek = notNullDays.reverse().find(d => d);
    
    if (lastDayOfWeek === undefined || lastDayOfWeek === null) 
      return false;
    
    const isFirstDayOfMonth = lastDayOfWeek.getDate() <= 7;

    return isFirstDayOfMonth;
  }
  public calculateWeeksInMonthsByWeek(week: (Date | null)[]) : number {
    const lastDayOfWeek = week
      .filter(d => d !== null)
      .reverse()
      .find(d => d);
    
    if (lastDayOfWeek === undefined || lastDayOfWeek === null) 
      throw new Error('Last day of week cannot be null or undefined.');
    
    const fromDay = lastDayOfWeek?.getDate();
    const maxDaysInMonths = 32 - new Date(
      lastDayOfWeek.getFullYear(), 
      lastDayOfWeek.getMonth(),
      32).getDate();
    const floor = Math.ceil((maxDaysInMonths - fromDay) / 7);
    let result = floor;

    if ((lastDayOfWeek.getMonth() !== 11 &&
    lastDayOfWeek.getMonth() !== 0) && 
      (maxDaysInMonths - fromDay) % 7 === 0) {
      ++result;
    }

    if ((lastDayOfWeek.getMonth() === 11 || 
      lastDayOfWeek.getMonth() === 0) && 
      maxDaysInMonths - fromDay % 7 !== 0) {
      ++result;
    }

    return result;
  }
}
