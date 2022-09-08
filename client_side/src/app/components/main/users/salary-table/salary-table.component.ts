import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { DefaultPaging, Role, UserStatuses } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { DialogService } from 'src/app/services/dialog.service';
import { UserService } from 'src/app/services/model-services/user.service';

@Component({
  selector: 'app-salary-table',
  templateUrl: './salary-table.component.html',
  styleUrls: ['./salary-table.component.scss']
})
export class SalaryTableComponent implements OnInit {

  displayedColumns = [
    'id',
    'fullName',
    'email',
    'role',
    'job',
    'startDay',
    'workingProgress',
    'lateTime',
    'salary'
  ]

  dataSource = new MatTableDataSource([] as User[]);
  paging = DefaultPaging;
  role = Role;
  userStatus = UserStatuses;
  onQueryUsers$ = new Subject();

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private readonly userService: UserService,
    private readonly dialog: DialogService
  ) { }

  ngOnInit(): void {
    this.onQueryUsers$.subscribe(res => {
      this.userService.getUserWorkingProgress().subscribe( res => {
        this.dataSource = new MatTableDataSource(res.users as User[]);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
    });

    this.onQueryUsers$.next(true);
  }

  openCreateUserDialog() {
    this.dialog.openCreateUser();

    this.dialog.confirmed().subscribe(res => {
      if (res) {
        this.onQueryUsers$.next(true);
      }
    })
  }

}
