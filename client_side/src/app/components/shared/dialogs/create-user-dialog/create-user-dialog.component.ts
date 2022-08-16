import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subject, withLatestFrom, startWith, map, filter, takeUntil } from 'rxjs';
import { Role } from 'src/app/common/constant';
import { Department } from 'src/app/models/department/department.model';
import { Job } from 'src/app/models/job/job.model';
import { CreateUserReq } from 'src/app/models/user/dtos/create-user-req';
import { UserData } from 'src/app/models/user/user-data';
import { DepartmentService } from 'src/app/services/model-services/department.service';
import { JobService } from 'src/app/services/model-services/job.service';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-create-user-dialog',
  templateUrl: './create-user-dialog.component.html',
  styleUrls: ['./create-user-dialog.component.scss']
})
export class CreateUserDialogComponent extends DialogComponent implements OnInit {
  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();

  roles = Object.values(Role);
  roleKeys = this.roles.slice(0, this.roles.length / 2);
  jobs: Job[] = [];
  departments: Department[] = [];

  createUserForm = new FormGroup({
    userName: new FormControl(''),
    password: new FormControl(''),
    rePassword: new FormControl(''),
    fullName: new FormControl(''),
    role: new FormControl(0),
    phoneNumber: new FormControl(''),
    email: new FormControl(''),
    departmentId: new FormControl(),
    jobId: new FormControl()
  })

  constructor(
    override dialogRef: MatDialogRef<CreateUserDialogComponent>,
    private readonly userService: UserService,
    private readonly notification: NotificationService,
    private readonly departmentService: DepartmentService,
    private readonly jobService: JobService
  ) { 
    super();
    this.jobService.getJobs().subscribe(res => {
      this.jobs = res.jobs;
    })
    this.departmentService.getDepartments().subscribe(res => {
      this.departments = res.departments;
    })
  }

  ngOnInit(): void {
    this.formSubmit$.pipe(
      withLatestFrom(this.createUserForm.valueChanges.pipe(startWith({}))),
        map(([, userValue]) => userValue),
        filter((value) => {
          if (!(!!value && !!value.userName)) {
            return false;
          }

          if(value.password != value.rePassword) {
            this.notification.showError("Mật khẩu nhập lại không trùng khớp");
            return false;
          }
          return true;
        }),
        takeUntil(this.destroyed$)
    ).subscribe(userValue => {
      this.userService.createUser({
        userName: userValue.userName,
        password: userValue.password,
        fullName: userValue.fullName,
        role: userValue.role,
        phoneNumber: userValue.phoneNumber,
        email: userValue.email,
        jobId: userValue.jobId,
        departmentId: userValue.departmentId
      } as CreateUserReq).subscribe(res => {
        this.confirm();
        this.notification.showSuccess("Tạo người dùng thành công");
      })
    })
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }

}
