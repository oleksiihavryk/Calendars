import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarsInformationComponent } from './calendars-information.component';

describe('CalendarsInformationComponent', () => {
  let component: CalendarsInformationComponent;
  let fixture: ComponentFixture<CalendarsInformationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CalendarsInformationComponent]
    });
    fixture = TestBed.createComponent(CalendarsInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
