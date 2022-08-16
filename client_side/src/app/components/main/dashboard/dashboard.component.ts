import { Component, OnInit } from '@angular/core';
import { UserStatuses } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/model-services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  constructor(
    private readonly userService: UserService
  ) { }

  userStatus = UserStatuses;

  usersCount = 0;
  registerCount = 0;
  absentCount = 0;
  unregisterCount = 0;

  ngOnInit(): void {
    this.userService.getUsersEvents().subscribe(res => {
      this.usersCount = res.users.length;
      this.counting(res.users);
    })
  }

  counting(users: User[]) {
    for (let user of users) {
      if (user.status == this.userStatus.Working) {
        this.registerCount ++;
        continue;
      }

      if (user.status == this.userStatus.Absent) {
        this.absentCount ++;
        continue;
      }

      this.unregisterCount++;
    }
  }

}
