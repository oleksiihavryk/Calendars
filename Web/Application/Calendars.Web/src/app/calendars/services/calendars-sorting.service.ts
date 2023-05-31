import { Injectable } from '@angular/core';
import { Calendar } from 'src/app/shared/domain/calendar';

interface ICalendarsSortingMechanism {
  compare(first: Calendar, second: Calendar): number;
}
interface ISetSortDirection {
  setDirection(value: SortDirection): void;
}

export enum SortDirection {
  Up,
  Down,
  None
}
@Injectable({
  providedIn: 'root'
})
export class CalendarsSortingService {
  public nameSortState: SortDirection = SortDirection.None;
  public typeSortState: SortDirection = SortDirection.None;
  public yearSortState: SortDirection = SortDirection.None;

  public sortYearDown(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return first.year - second.year;
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.yearSortState = value;
      },
    }, SortDirection.Down);
  }
  public sortYearUp(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return second.year - first.year;
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.yearSortState = value;
      },
    }, SortDirection.Up);
  }
  public sortTypeDown(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return first.type - second.type;
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.typeSortState = value;
      },
    }, SortDirection.Down);
  }
  public sortTypeUp(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return second.type - first.type;
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.typeSortState = value;
      },
    }, SortDirection.Up);
  }
  public sortNameDown(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return first.name.localeCompare(second.name); 
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.nameSortState = value;
      },
    }, SortDirection.Down);
  }
  public sortNameUp(value: Calendar[]) {
    this.sort(value, {
      compare(first, second) {
        return -(first.name.localeCompare(second.name));
      },
    });
    this.changeState({
      setDirection: (value) => {
        this.nameSortState = value;
      },
    }, SortDirection.Up);
  }
  public removeSorting() {
    this.changeState({ setDirection(value) { } })
  }

  private sort(value: Calendar[], mechanism: ICalendarsSortingMechanism) {
    value.sort(mechanism.compare);
  }
  private changeState(
    setSortDirection: ISetSortDirection, 
    value: SortDirection | undefined = undefined) {
    this.yearSortState = SortDirection.None;
    this.nameSortState = SortDirection.None;
    this.typeSortState = SortDirection.None;

    setSortDirection.setDirection(value ?? SortDirection.None);
  }
}
