import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileInputComponent } from './profile-input.component';

describe('ProfileInputComponent', () => {
  let component: ProfileInputComponent;
  let fixture: ComponentFixture<ProfileInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfileInputComponent]
    });
    fixture = TestBed.createComponent(ProfileInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
