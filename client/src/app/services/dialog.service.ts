import { Overlay, RepositionScrollStrategy } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable, take, map } from 'rxjs';
import { ConfirmationComponent } from '../components/shared/dialogs/confirmation/confirmation.component';
import { CreateEventComponent } from '../components/shared/dialogs/create-event/create-event.component';
import { CreateVacationComponent } from '../components/shared/dialogs/create-vacation/create-vacation.component';
import { DialogComponent } from '../components/shared/dialogs/dialog.component';
import { EditProfileComponent } from '../components/shared/dialogs/edit-profile/edit-profile.component';
import { UnconfirmedEventsDialogComponent } from '../components/shared/dialogs/unconfirmed-events-dialog/unconfirmed-events-dialog.component';
import { DialogData } from '../models/dialog-data';
import { User } from '../models/user/user.model';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  scrollStrategy: RepositionScrollStrategy;

  constructor(
    private readonly dialog: MatDialog,
    private readonly overlay: Overlay
  ) { 
    this.scrollStrategy = this.overlay.scrollStrategies.reposition();
  }

  dialogRef!: MatDialogRef<DialogComponent>;

  public openConfirm(data: DialogData) {
    this.dialogRef = this.dialog.open(ConfirmationComponent, {
      data: data
    })
  }

  public openCreateVacation() {
    this.dialogRef = this.dialog.open(CreateVacationComponent, {
      width: '700px'
    })
  }

  public openCreateEvent() {
    this.dialogRef = this.dialog.open(CreateEventComponent, {
      width: '700px'
    })
  }

  public openUnconfirmedEvents(user: User) {
    this.dialogRef = this.dialog.open(UnconfirmedEventsDialogComponent, {
      data: user,
      width: '700px'
    })
  }

  public openEditUser(user: User) {
    this.dialogRef = this.dialog.open(EditProfileComponent, {
      data: user,
      width: '700px'
    })
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
