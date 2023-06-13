import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteCalendarComponent } from './delete-calendar.component';

describe('DeleteCalendarComponent', () => {
  let component: DeleteCalendarComponent;
  let fixture: ComponentFixture<DeleteCalendarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteCalendarComponent]
    });
    fixture = TestBed.createComponent(DeleteCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
