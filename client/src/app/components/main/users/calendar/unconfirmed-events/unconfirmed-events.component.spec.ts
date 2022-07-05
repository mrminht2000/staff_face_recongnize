import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnconfirmedEventsComponent } from './unconfirmed-events.component';

describe('UnconfirmedEventsComponent', () => {
  let component: UnconfirmedEventsComponent;
  let fixture: ComponentFixture<UnconfirmedEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnconfirmedEventsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnconfirmedEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
