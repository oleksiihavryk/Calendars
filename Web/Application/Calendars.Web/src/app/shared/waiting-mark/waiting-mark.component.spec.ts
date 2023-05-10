import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WaitingMarkComponent } from './waiting-mark.component';

describe('WaitingMarkComponent', () => {
  let component: WaitingMarkComponent;
  let fixture: ComponentFixture<WaitingMarkComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WaitingMarkComponent]
    });
    fixture = TestBed.createComponent(WaitingMarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
