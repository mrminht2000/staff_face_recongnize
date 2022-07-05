import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnconfirmedEventsDialogComponent } from './unconfirmed-events-dialog.component';

describe('UnconfirmedEventsComponent', () => {
  let component: UnconfirmedEventsDialogComponent;
  let fixture: ComponentFixture<UnconfirmedEventsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnconfirmedEventsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnconfirmedEventsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
