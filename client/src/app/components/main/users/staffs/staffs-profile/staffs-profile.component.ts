import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { DefaultPaging, EventType, Role } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { Event } from 'src/app/models/event/event.model';
import { UserService } from 'src/app/services/model-services/user.service';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { EventService } from 'src/app/services/model-services/event.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-staffs-profile',
  templateUrl: './staffs-profile.component.html',
  styleUrls: ['./staffs-profile.component.scss']
})
export class StaffsProfileComponent implements OnInit, OnDestroy {
  userId = 0;
  user = {} as User;
  role = Role;
  routeSub = new Subscription();
  displayedEventColumns = [
    'id',
    'eventName',
    'startTime',
    'endTime',
    'actions'
  ]

  dataEventSource = new MatTableDataSource([] as Event[]);

  paging = DefaultPaging;

  eventType = EventType;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly userService: UserService,
    private readonly eventService: EventService,
    private readonly authService: AuthenticationService,
    private readonly notification: NotificationService
  ) { 
    this.routeSub = this.activatedRoute.params.subscribe(params => {
      this.userId = params['id'];
    })
  }

  ngOnInit(): void {
    this.userService.getUserById(this.userId).subscribe(res => {
      this.user = res;
    })
  }

  onUnconfirmedEvents(){
    this.eventService.getUnconfirmEventsByUserId(this.user.id).subscribe(res => {
      this.dataEventSource = new MatTableDataSource(res.events as Event[]);
      this.dataEventSource.sort = this.sort;
      this.dataEventSource.paginator = this.paginator;
    })
  }

  ngOnDestroy(): void {
      this.routeSub.unsubscribe();
  }

  isAdmin() {
    return (this.authService.currentUser.role >= Role.Admin);
  }

  isAdminOrOwner() {
    return (this.user.id == this.authService.currentUser.id || this.isAdmin());
  }

  acceptEvent(event: Event) {
    event.isConfirmed = true;
    this.eventService.acceptEvent(event).subscribe(res => {
      this.notification.showSuccess("Đồng ý ngày nghỉ thành công");
      this.onUnconfirmedEvents();
    })
  }

  deleteEvent(event: Event) {
    this.eventService.declineEvent(event.id).subscribe(res => {
      this.notification.showError("Đã từ chối ngày nghỉ");
      this.onUnconfirmedEvents();
    })
  }
}
