import { Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DefaultPaging, EventType, Role } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { Event } from 'src/app/models/event/event.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { EventService } from 'src/app/services/model-services/event.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';
import { DialogService } from 'src/app/services/dialog.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-events-list-dialog',
  templateUrl: './events-list-dialog.component.html',
  styleUrls: ['./events-list-dialog.component.scss'],
})
export class EventsListDialogComponent
  extends DialogComponent
  implements OnInit, OnDestroy
{
  constructor(
    override dialogRef: MatDialogRef<EventsListDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User,
    private readonly eventService: EventService,
    private readonly authService: AuthenticationService,
    private readonly notification: NotificationService,
    private readonly dialog: DialogService
  ) {
    super();
  }

  role = Role;
  displayedEventColumns = [
    'id',
    'eventName',
    'startTime',
    'endTime',
    'actions',
  ];
  dataEventSource = new MatTableDataSource([] as Event[]);
  paging = DefaultPaging;
  eventType = EventType;
  onQueryEvents$ = new Subject<boolean>();
  destroyed$ = new Subject<boolean>();

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.dataEventSource = new MatTableDataSource(this.user.events as Event[]);
    this.dataEventSource.sort = this.sort;
    this.dataEventSource.paginator = this.paginator;

    this.onQueryEvents$.pipe(
      takeUntil(this.destroyed$)
      ).subscribe((res) => {
      this.eventService
        .getUnconfirmEventsByUserId(this.user.id)
        .subscribe((res) => {
          this.dataEventSource = new MatTableDataSource(res.events as Event[]);
          this.dataEventSource.sort = this.sort;
          this.dataEventSource.paginator = this.paginator;
        });
    });
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
  }

  onQueryEvent() {}

  isAdmin() {
    return this.authService.currentUser.role >= Role.Admin;
  }

  isAdminOrOwner() {
    return this.user.id == this.authService.currentUser.id || this.isAdmin();
  }

  acceptEvent(event: Event) {
    this.dialog.openConfirm({
      title: 'X??c nh???n',
      message: 'B???n c?? x??c nh???n cho nh??n vi??n ngh??? ng??y n??y kh??ng ?',
    });
    this.dialog.confirmed().subscribe((confirmed) => {
      if (confirmed) {
        event.isConfirmed = true;
        this.eventService.acceptEvent(event).subscribe((res) => {
          this.notification.showSuccess('?????ng ?? ng??y ngh??? th??nh c??ng');
          this.onQueryEvents$.next(true);
        });
      }
    });
  }

  deleteEvent(event: Event) {
    this.dialog.openConfirm({
      title: 'X??c nh???n',
      message: 'B???n c?? x??c nh???n t??? ch???i nh??n vi??n ngh??? ng??y n??y kh??ng ?',
    });

    this.dialog.confirmed().subscribe((confirmed) => {
      if (confirmed) {
        this.eventService.deleteEvent(event.id).subscribe((res) => {
          this.notification.showError('???? t??? ch???i ng??y ngh???');
          this.onQueryEvents$.next(true);
        });
      }
    });
  }
}
