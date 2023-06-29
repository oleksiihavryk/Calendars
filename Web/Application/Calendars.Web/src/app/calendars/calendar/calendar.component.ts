import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, mergeMap } from 'rxjs';
import { Calendar } from 'src/app/shared/domain/calendar';
import { CalendarsService } from '../services/calendars.service';
import { ModalService } from 'src/app/shared/services/modal.service';
import { IResponse } from 'src/app/shared/services/resources-http-client';
import { Day } from 'src/app/shared/domain/day';

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

  public dayBackgroundColorByDate(date: Date | null): string {
    const day = this.findDayByDate(date);
    const num = day?.backgroundArgbColorInteger;
    
    if (num === undefined)
      return 'unset';

    const a = (num << 0) >>> 24,
            r = (num << 8) >>> 24,
            g = (num << 16) >>> 24, 
            b = (num << 24) >>> 24;
    const format = this.decimalNumTo16string;

    return '#'+format(r)+format(g)+format(b)+format(a);
  }
  public dayTextColorByDate(date: Date | null): string {
    const day = this.findDayByDate(date);
    const num = day?.textArgbColorInteger;

    if (num === undefined)
      return 'unset';

    const a = (num << 0) >>> 24,
            r = (num << 8) >>> 24,
            g = (num << 16) >>> 24, 
            b = (num << 24) >>> 24;
    const format = this.decimalNumTo16string;
    return '#'+format(r)+format(g)+format(b)+format(a);
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
    let weeks = [];
    let months = [];

    weeks = this.createWeeksByYear(year);
    months = this.createMonthsByWeeks(weeks);

    return months;
  }
  public createRedirectToCalendarsFunction(): () => void {
    return () => {
      this.router.navigateByUrl('/calendars');
    };
  }
  public navigateToDay(day: Date | null): void {
    if (day !== null) {
      this.router.navigate(
        ['month', day.getMonth(), 'day', day.getDate()],
        {
          relativeTo: this.route
        })
    }
  }
  public isBeginOfMonth(week: (Date | null)[]) : boolean {
    const notNullDays = week.filter(d => d !== null);
    const lastDayOfWeek = notNullDays.reverse().find(d => d);
    
    if (lastDayOfWeek === undefined || lastDayOfWeek === null) 
      return false;
    
    const isFirstDayOfMonth = lastDayOfWeek.getDate() <= 7;

    return isFirstDayOfMonth;
  }
  public createWeeksByYear(year: Date[]) : (Date | null)[][] {
    let weeks = [];
    let spanIndex = year[0].getDay();
    const weeksCount = 53;

    const firstWeek: (Date | null)[] = [];

    for (let j = 0; j < spanIndex; j++) {
      firstWeek.push(null);
    }

    year.slice(0, 7 - spanIndex).map(d => firstWeek.push(d));
    weeks.push(firstWeek);

    if (spanIndex === 0) {
      for(let i = 1; i <= weeksCount; i++) {
        let index = i * 7;
        weeks.push(year.slice(index, index + 7));
      }
    } else {
      for(let i = 0; i < weeksCount; i++) {
        let index = i * 7 + (7 - spanIndex);
        weeks.push(year.slice(index, index + 7));
      }
    }

    if (weeks[weeksCount - 1][0] !== null) {
      const lastWeekLength = weeks[weeksCount - 1].length;
      for (let j = 0; j < 7 - lastWeekLength; j++) 
        weeks[weeksCount - 1].push(null);
    } else {
      const lastWeekLength = weeks[weeksCount - 2].length;
      for (let j = 0; j < 7 - lastWeekLength; j++) 
        weeks[weeksCount - 2].push(null);
    }

    return weeks;
  }
  public createMonthsByWeeks(weeks: (Date | null)[][])
    : (Date | null)[][][] { 
    let months: (Date | null)[][][] = [];
    let monthsCount = 12;
    let firstWeekOfMonth = 0;

    while (monthsCount --> 0) {
      const week = weeks[firstWeekOfMonth];
      const weeksSpanIndex = this.calculateWeeksInMonthsByFirstWeek(week)
      const firstWeekOfNextMonth = firstWeekOfMonth + weeksSpanIndex;
      const weeksOfMonths = weeks
        .slice(firstWeekOfMonth, firstWeekOfNextMonth);
      months.push(weeksOfMonths);
      firstWeekOfMonth = firstWeekOfNextMonth;
    }

    return months;
  }
  public calculateWeeksInMonthsByFirstWeek(week: (Date | null)[]) : number {
    const lastDayOfWeek = week
      .filter(d => d !== null)
      .reverse()
      .find(d => d);
    
    if (lastDayOfWeek === undefined || lastDayOfWeek === null) 
      throw new Error('Last day of week cannot be null or undefined.');
    
    if (lastDayOfWeek.getDate() > 7) 
      throw new Error('Passed week is not a first week of months.');

    const fromDay = lastDayOfWeek?.getDate();
    const maxDaysInMonths = 32 - new Date(
      lastDayOfWeek.getFullYear(), 
      lastDayOfWeek.getMonth(),
      32).getDate();
    let result = 0;

    if (lastDayOfWeek.getMonth() === 0) {
      result = 1 + Math.floor((maxDaysInMonths - fromDay) / 7);
    } else if (lastDayOfWeek.getMonth() === 11) {
      result = 2 + Math.floor((maxDaysInMonths - fromDay) / 7);
    } else {
      result = Math.ceil((maxDaysInMonths - fromDay) / 7) + 
        Number((maxDaysInMonths - fromDay) % 7 === 0);
    }

    return result;
  }

  private decimalNumTo16string(num: number): string {
    let result = num <= 16 ? '0' : '';
    result += num.toString(16);
    return result;
  } 
  private findDayByDate(date: Date | null): Day | undefined {
    return this.calendar?.days.find(
      d => {
        const dayDate = new Date(this.calendar?.year ?? 0, 0, d.dayNumber);
        return dayDate.toUTCString() === date?.toUTCString();
      });
  }
}
