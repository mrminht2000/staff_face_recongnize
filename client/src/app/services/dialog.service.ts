import { Overlay } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable, take, map } from 'rxjs';
import { ChangePasswordComponent } from '../components/shared/dialogs/change-password/change-password.component';
import { ConfirmationComponent } from '../components/shared/dialogs/confirmation/confirmation.component';
import { CreateEventComponent } from '../components/shared/dialogs/create-event/create-event.component';
import { CreateUserComponent } from '../components/shared/dialogs/create-user/create-user.component';
import { CreateVacationComponent } from '../components/shared/dialogs/create-vacation/create-vacation.component';
import { DialogComponent } from '../components/shared/dialogs/dialog.component';
import { EditProfileComponent } from '../components/shared/dialogs/edit-profile/edit-profile.component';
import { ProfileUserComponent } from '../components/shared/dialogs/profile-user/profile-user.component';
import { UnconfirmedEventsDialogComponent } from '../components/shared/dialogs/unconfirmed-events-dialog/unconfirmed-events-dialog.component';
import { DialogData } from '../models/dialog-data';
import { User } from '../models/user/user.model';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(
    private readonly dialog: MatDialog,
    private readonly overlay: Overlay
  ) {
  }

  dialogRef!: MatDialogRef<DialogComponent>;

  openConfirm(data: DialogData) {
    this.dialogRef = this.dialog.open(ConfirmationComponent, {
      data: data
    });
  }

  openCreateVacation() {
    this.dialogRef = this.dialog.open(CreateVacationComponent, {
      width: '700px'
    });
  }

  openCreateEvent() {
    this.dialogRef = this.dialog.open(CreateEventComponent, {
      width: '700px'
    });
  }

  openUnconfirmedEvents(user: User) {
    this.dialogRef = this.dialog.open(UnconfirmedEventsDialogComponent, {
      data: user,
      width: '700px'
    });
  }

  openCreateUser() {
    this.dialogRef = this.dialog.open(CreateUserComponent, {
      width: '700px'
    });
  }

  openProfileUser(user: User) {
    this.dialogRef = this.dialog.open(ProfileUserComponent, {
      data: user,
      width: '700px'
    });
  }

  openEditUser(user: User) {
    this.dialogRef = this.dialog.open(EditProfileComponent, {
      data: user,
      width: '700px'
    })
  }

  openChangePassword(user: User) {
    this.dialogRef = this.dialog.open(ChangePasswordComponent, {
      data: user,
      width: '700px'
    });
  }

  public confirmed(): Observable<any> { 
    return this.dialogRef.afterClosed().pipe(
      take(1), 
      map(res => { 
        return res; 
      }
    ));
  }
}
