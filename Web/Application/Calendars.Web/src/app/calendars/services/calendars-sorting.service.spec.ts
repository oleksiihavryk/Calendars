import { TestBed } from '@angular/core/testing';

import { CalendarsSortingService } from './calendars-sorting.service';

describe('CalendarsSortingService', () => {
  let service: CalendarsSortingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CalendarsSortingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
