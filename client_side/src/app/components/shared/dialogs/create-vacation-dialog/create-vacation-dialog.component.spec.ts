import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVacationDialogComponent } from './create-vacation-dialog.component';

describe('CreateVacationDialogComponent', () => {
  let component: CreateVacationDialogComponent;
  let fixture: ComponentFixture<CreateVacationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateVacationDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateVacationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
