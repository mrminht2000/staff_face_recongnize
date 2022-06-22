import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffsCalendarComponent } from './staffs-calendar.component';

describe('CalendarComponent', () => {
  let component: StaffsCalendarComponent;
  let fixture: ComponentFixture<StaffsCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffsCalendarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StaffsCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
