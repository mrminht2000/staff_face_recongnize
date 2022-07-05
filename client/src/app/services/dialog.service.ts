import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable, take, map } from 'rxjs';
import { ConfirmationComponent } from '../components/shared/dialogs/confirmation/confirmation.component';
import { CreateEventComponent } from '../components/shared/dialogs/create-event/create-event.component';
import { CreateVacationComponent } from '../components/shared/dialogs/create-vacation/create-vacation.component';
import { DialogComponent } from '../components/shared/dialogs/dialog.component';
import { UnconfirmedEventsDialogComponent } from '../components/shared/dialogs/unconfirmed-events-dialog/unconfirmed-events-dialog.component';
import { DialogData } from '../models/dialog-data';
import { User } from '../models/user/user.model';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(
    private readonly dialog: MatDialog
  ) { }

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

  public confirmed(): Observable<any> { 
    return this.dialogRef.afterClosed().pipe(
      take(1), 
      map(res => { 
        return res; 
      }
    ));
  }
}
