import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject, withLatestFrom, map, takeUntil, filter, startWith, tap } from 'rxjs';
import { Role } from 'src/app/common/constant';
import { UserData } from 'src/app/models/user/user-data';
import { User } from 'src/app/models/user/user.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DialogService } from 'src/app/services/dialog.service';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent extends DialogComponent implements OnInit {

  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();

  roles = Object.values(Role);
  roleKeys = this.roles.slice(0, this.roles.length / 2);

  editUserForm = new FormGroup({
    userName: new FormControl(this.user.userName),
    fullName: new FormControl(this.user.fullName),
    role: new FormControl(this.user.role),
    phoneNumber: new FormControl(this.user.phoneNumber),
    email: new FormControl(this.user.email),
    departmentId: new FormControl(this.user.department.id),
    jobId: new FormControl(this.user.job.id)
  })

  constructor(
    override dialogRef: MatDialogRef<EditProfileComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User,
    private readonly authService: AuthenticationService,
    private readonly userService: UserService,
    private readonly notification: NotificationService,
    private readonly dialog: DialogService
  ) { 
    super();
  }

  ngOnInit(): void {
    this.formSubmit$.pipe(
      withLatestFrom(this.editUserForm.valueChanges.pipe(startWith({}))),
        map(([, userValue]) => userValue as UserData),
        filter((value) => {
          return !!value && !!value.userName;
        }),
        tap(value => {
          value.id = this.user.id;
        }),
        takeUntil(this.destroyed$)
    ).subscribe(userValue => {
      this.userService.updateUser(userValue).subscribe(res => {
        this.confirm();
        this.notification.showSuccess("Thay đổi thông tin thành công");
      })
    })
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }

}
