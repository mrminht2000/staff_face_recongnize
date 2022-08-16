import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileUserDialogComponent } from './profile-user-dialog.component';

describe('ProfileUserDialogComponent', () => {
  let component: ProfileUserDialogComponent;
  let fixture: ComponentFixture<ProfileUserDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileUserDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
