import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { Role } from 'src/app/common/constant';
import { toUserData } from 'src/app/common/user-helper';
import { User } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-profile-user-dialog',
  templateUrl: './profile-user-dialog.component.html',
  styleUrls: ['./profile-user-dialog.component.scss']
})
export class ProfileUserDialogComponent extends DialogComponent implements OnInit {
  destroyed$ = new Subject<void>();

  roles = Role;
  constructor(
    override dialogRef: MatDialogRef<ProfileUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User,
    private readonly userService: UserService,
    private readonly notification: NotificationService
  ) { 
    super();
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }

  confirmUser() {
    this.user.isConfirmed = true;
    this.userService.updateUser(toUserData(this.user)).subscribe(res => {
      this.notification.showSuccess("Xác nhận người dùng thành công");
      this.confirm();
    })
  }
}
