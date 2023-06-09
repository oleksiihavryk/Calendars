import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthsNavigatorComponent } from './months-navigator.component';

describe('MonthsNavigatorComponent', () => {
  let component: MonthsNavigatorComponent;
  let fixture: ComponentFixture<MonthsNavigatorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MonthsNavigatorComponent]
    });
    fixture = TestBed.createComponent(MonthsNavigatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
