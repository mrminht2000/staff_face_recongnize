import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalaryTableComponent } from './salary-table.component';

describe('SalaryTableComponent', () => {
  let component: SalaryTableComponent;
  let fixture: ComponentFixture<SalaryTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SalaryTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SalaryTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
