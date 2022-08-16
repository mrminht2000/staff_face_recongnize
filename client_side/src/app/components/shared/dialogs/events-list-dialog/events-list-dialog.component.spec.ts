import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventsListDialogComponent } from './events-list-dialog.component';

describe('EventsListDialogComponent', () => {
  let component: EventsListDialogComponent;
  let fixture: ComponentFixture<EventsListDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventsListDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventsListDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
