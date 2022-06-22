import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-staffs-list',
  templateUrl: './staffs-list.component.html',
  styleUrls: ['./staffs-list.component.scss']
})
export class StaffsListComponent implements OnInit {
  displayedColumns = [
    'id',
    'fullName',
    'workStatus',
    'workDays'
  ]
  constructor() { }

  ngOnInit(): void {
  }

}
