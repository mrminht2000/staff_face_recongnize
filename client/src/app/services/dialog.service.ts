import { Overlay } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable, take, map } from 'rxjs';
import { ChangePasswordDialogComponent } from '../components/shared/dialogs/change-password-dialog/change-password-dialog.component';
import { ConfirmationDialogComponent } from '../components/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { CreateEventDialogComponent } from '../components/shared/dialogs/create-event-dialog/create-event-dialog.component';
import { CreateUserDialogComponent } from '../components/shared/dialogs/create-user-dialog/create-user-dialog.component';
import { CreateVacationDialogComponent } from '../components/shared/dialogs/create-vacation-dialog/create-vacation-dialog.component';
import { DialogComponent } from '../components/shared/dialogs/dialog.component';
import { EditProfileDialogComponent } from '../components/shared/dialogs/edit-profile-dialog/edit-profile-dialog.component';
import { EventDetailDialogComponent } from '../components/shared/dialogs/event-detail-dialog/event-detail-dialog.component';
import { ProfileUserDialogComponent } from '../components/shared/dialogs/profile-user-dialog/profile-user-dialog.component';
import { EventsListDialogComponent } from '../components/shared/dialogs/events-list-dialog/events-list-dialog.component';
import { DialogData } from '../models/dialog-data';
import { Event } from '../models/event/event.model';
import { User } from '../models/user/user.model';
import { UpdateEventDialogComponent } from '../components/shared/dialogs/update-event-dialog/update-event-dialog.component';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(
    private readonly dialog: MatDialog,
    private readonly authService: AuthenticationService
  ) {
  }

  dialogRef!: MatDialogRef<DialogComponent>;

  openConfirm(data: DialogData) {
    this.dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: data
    });
  }

  openCreateVacation(isCompanyEvent?: boolean) {
    this.dialogRef = this.dialog.open(CreateVacationDialogComponent, {
      data: isCompanyEvent ? null : this.authService.currentUser.id,
      width: '700px'
    });
  }

  openCreateEvent(isCompanyEvent?: boolean) {
    this.dialogRef = this.dialog.open(CreateEventDialogComponent, {
      data: isCompanyEvent ? null : this.authService.currentUser.id,
      width: '700px'
    });
  }

  openEventDetail(event: Event) {
    this.dialogRef = this.dialog.open(EventDetailDialogComponent, {
      data: event,
      width: '500px'
    })
  }

  openUnconfirmedEvents(user: User) {
    this.dialogRef = this.dialog.open(EventsListDialogComponent, {
      data: user,
      width: '700px'
    });
  }

  openEditEvent(event: Event) {
    this.dialogRef = this.dialog.open(UpdateEventDialogComponent, {
      data: event,
      width: '700px'
    })
  }

  openCreateUser() {
    this.dialogRef = this.dialog.open(CreateUserDialogComponent, {
      width: '700px'
    });
  }

  openProfileUser(user: User) {
    this.dialogRef = this.dialog.open(ProfileUserDialogComponent, {
      data: user,
      width: '700px'
    });
  }

  openEditUser(user: User) {
    this.dialogRef = this.dialog.open(EditProfileDialogComponent, {
      data: user,
      width: '700px'
    })
  }

  openChangePassword(user: User) {
    this.dialogRef = this.dialog.open(ChangePasswordDialogComponent, {
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
