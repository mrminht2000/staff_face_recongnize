import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from '../dialog.component';
import { EventService } from 'src/app/services/model-services/event.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogService } from 'src/app/services/dialog.service';
import { Event } from 'src/app/models/event/event.model';
import { EventType, Role } from 'src/app/common/constant';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-event-detail-dialog',
  templateUrl: './event-detail-dialog.component.html',
  styleUrls: ['./event-detail-dialog.component.scss']
})
export class EventDetailDialogComponent extends DialogComponent implements OnInit {

  constructor(
    override dialogRef: MatDialogRef<EventDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public event: Event,
    private readonly eventService: EventService,
    private readonly notification: NotificationService,
    private readonly dialog: DialogService,
    private readonly authService: AuthenticationService
  ) { 
    super();
  }

  ngOnInit(): void {
  }

  deleteEvent() {
    this.eventService.deleteEvent(this.event.id).subscribe(res => {
      this.notification.showError("Đã xoá sự kiện");
      this.confirm();
    })
  }

  editEvent() {
    this.dialog.openEditEvent(this.event);
    this.dialog.confirmed().subscribe(res => {
      if (res) {
        this.confirm();
      }
    })
  }

  editableEvent() {
    return this.event.eventType == EventType.Default || 
           this.event.isConfirmed == false || 
           this.authService.currentUser.role >= Role.Admin;
  }
}
