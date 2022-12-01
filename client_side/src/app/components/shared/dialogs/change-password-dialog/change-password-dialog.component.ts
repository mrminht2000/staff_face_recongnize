import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { filter, map, startWith, Subject, takeUntil, tap, withLatestFrom } from 'rxjs';
import { ChangePasswordReq } from 'src/app/models/user/dtos/change-password-req';
import { UserData } from 'src/app/models/user/user-data';
import { User } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss']
})
export class ChangePasswordDialogComponent extends DialogComponent implements OnInit {
  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();

  changePasswordForm = new FormGroup({
    oldPassword: new FormControl(),
    newPassword: new FormControl(),
    rePassword: new FormControl
  })

  constructor(
    override dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User,
    private readonly userService: UserService,
    private readonly notification: NotificationService
  ) { 
    super();
  }

  ngOnInit(): void {
    this.formSubmit$.pipe(
      withLatestFrom(this.changePasswordForm.valueChanges.pipe(startWith({}))),
        map(([, changePassWordValue]) => changePassWordValue as ChangePasswordReq),
        filter((value) => {
          return !!value && !!value.newPassword && !!value.rePassword;
        }),
        tap(value => {
          value.userId = this.user.id;
        }),
        takeUntil(this.destroyed$)
    ).subscribe(changePassWordValue => {
      this.userService.changePassword(changePassWordValue).subscribe(res => {
        this.confirm();
        this.notification.showSuccess("Thay đổi mật khẩu thành công");
      })
    })
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
