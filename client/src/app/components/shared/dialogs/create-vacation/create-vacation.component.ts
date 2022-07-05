import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  filter,
  map,
  startWith,
  Subject,
  takeUntil,
  withLatestFrom,
} from 'rxjs';
import { CreateEventReq } from 'src/app/models/event/dtos/create-event-req';
import { VacationValue } from 'src/app/models/event/vacation-value';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { EventService } from 'src/app/services/model-services/event.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { EventType } from 'src/app/common/constant';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

export const MY_FORMATS = {
  parse: {
    dateInput: 'DD-MM-YYYY',
  },
  display: {
    dateInput: 'DD-MM-YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-create-vacation',
  templateUrl: './create-vacation.component.html',
  styleUrls: ['./create-vacation.component.scss'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }],
})
export class CreateVacationComponent extends DialogComponent implements OnInit, OnDestroy {
  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();

  createEventForm = new FormGroup({
    eventName: new FormControl(''),
    startTime: new FormControl(''),
    endTime: new FormControl(''),
  });

  constructor(
    override dialogRef: MatDialogRef<CreateVacationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CreateEventReq,
    private readonly eventService: EventService,
    private readonly authService: AuthenticationService,
    private readonly router: Router,
    private readonly notification: NotificationService
  ) {
    super ();
  }

  ngOnInit() {
    this.formSubmit$
      .pipe(
        withLatestFrom(this.createEventForm.valueChanges.pipe(startWith({}))),
        map(([, eventValue]) => eventValue as VacationValue),
        filter((value) => {
          if (!(!!value && !!value.eventName && !!value.startTime)) {
            return false;
          }

          if (value.startTime.setHours(0, 0, 0, 0) < (new Date()).setHours(0, 0, 0, 0) || (!!value.endTime && value.startTime > value.endTime)){
            this.notification.showError("Ngày nghỉ không hợp lệ");
            return false;
          }

          return true;
        }),
        takeUntil(this.destroyed$)
      )
      .subscribe((eventValue) => {
        this.eventService
          .createVacationByUser({
            eventName: eventValue.eventName,
            startTime: eventValue.startTime,
            endTime: eventValue.endTime,
            allDay: true,
            userId: this.authService.currentUser.id,
            eventType: EventType.Vacation,
          } as CreateEventReq)
          .subscribe((res) => {
            this.confirm();
            this.notification.showSuccess('Thêm ngày nghỉ thành công');
          });
      });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
