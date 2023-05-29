import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'calendarType'
})
export class CalendarTypePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    switch (value) {
        case 0: {
            return 'Julian';
        }  
        case 1: {
            return 'Gregorian'
        }
        default: {
            return 'unknown';
        }
    }
  }

}
