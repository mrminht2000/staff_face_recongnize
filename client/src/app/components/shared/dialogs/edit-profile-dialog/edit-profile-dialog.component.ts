import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject, withLatestFrom, map, takeUntil, filter, startWith, tap } from 'rxjs';
import { Role } from 'src/app/common/constant';
import { Department } from 'src/app/models/department/department.model';
import { Job } from 'src/app/models/job/job.model';
import { UserData } from 'src/app/models/user/user-data';
import { User } from 'src/app/models/user/user.model';
import { DepartmentService } from 'src/app/services/model-services/department.service';
import { JobService } from 'src/app/services/model-services/job.service';
import { UserService } from 'src/app/services/model-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-edit-profile-dialog',
  templateUrl: './edit-profile-dialog.component.html',
  styleUrls: ['./edit-profile-dialog.component.scss']
})
export class EditProfileDialogComponent extends DialogComponent implements OnInit {

  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();
  jobs: Job[] = [];
  departments: Department[] = [];

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
    override dialogRef: MatDialogRef<EditProfileDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User,
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
