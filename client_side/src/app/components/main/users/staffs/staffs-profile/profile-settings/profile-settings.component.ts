import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user/user.model';
import { DialogService } from 'src/app/services/dialog.service';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent implements OnInit {
  constructor(
    private readonly dialog: DialogService,
    private readonly userService: UserService,
    private readonly router: Router,
    private readonly notification: NotificationService
  ) { }

  @Input() user!: User;

  ngOnInit(): void {
  }

  openEditProfileDialog() {
    this.dialog.openEditUser(this.user);
  }

  openProfileDialog() {
    this.dialog.openProfileUser(this.user);
  }
  
  deleteUser(){
    this.dialog.openConfirm({
      title: 'Xoá người dùng',
      message: 'Bạn chắc chắn muốn xoá tài khoản ' + this.user.userName + ' ?'
    })

    this.dialog.confirmed().subscribe(res => {
      if (res) {
        this.userService.deleteUser(this.user.id).subscribe(res => {
          this.notification.showInfo("Đã xoá người dùng");
          this.router.navigate(['staffs/all']);
        })
      }
    })
  }
}
