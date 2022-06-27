import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DefaultPaging, Role } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/model-services/user.service';

@Component({
  selector: 'app-staffs-list',
  templateUrl: './staffs-list.component.html',
  styleUrls: ['./staffs-list.component.scss']
})
export class StaffsListComponent implements OnInit{
  displayedColumns = [
    'id',
    'fullName',
    'phoneNumber',
    'email',
    'role',
    'startDay',
    'workingProgress',
    'workingStatus',
    'isConfirmed'
  ]

  dataSource = new MatTableDataSource([] as User[]);

  paging = DefaultPaging;

  role = Role;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private readonly userService: UserService
  ) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe( res => {
      this.dataSource = new MatTableDataSource(res.users as User[]);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    })
  }
}
