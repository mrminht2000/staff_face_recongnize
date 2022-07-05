import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DefaultPaging, Role } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { EventService } from 'src/app/services/model-services/event.service';
import { UserService } from 'src/app/services/model-services/user.service';

@Component({
  selector: 'app-unconfirmed-events',
  templateUrl: './unconfirmed-events.component.html',
  styleUrls: ['./unconfirmed-events.component.scss']
})
export class UnconfirmedEventsComponent implements OnInit {

  displayedColumns = [
    'id',
    'fullName',
    'role',
    'workingProgress',
    'workingStatus',
    'unconfirmedEvents'
  ]

  dataSource = new MatTableDataSource([] as User[]);

  paging = DefaultPaging;

  role = Role;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private readonly userService: UserService,
    private readonly eventService: EventService
  ) { }

  ngOnInit(): void {
    this.eventService.getUnconfirmEvents().subscribe( res => {
      this.dataSource = new MatTableDataSource(res.users as User[]);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    })
  }

  countEvent(user: User) {
    return user.events.length;
  }

}
