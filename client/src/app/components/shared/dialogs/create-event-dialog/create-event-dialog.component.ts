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
import { EventService } from 'src/app/services/model-services/event.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { EventType } from 'src/app/common/constant';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { EventValue } from 'src/app/models/event/event-value';
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
  selector: 'app-create-event-dialog',
  templateUrl: './create-event-dialog.component.html',
  styleUrls: ['./create-event-dialog.component.scss']
})
export class CreateEventDialogComponent extends DialogComponent implements OnInit, OnDestroy {
  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();

  createEventForm = new FormGroup({
    eventName: new FormControl(''),
    startTime: new FormControl(''),
    endTime: new FormControl(''),
    allDay: new FormControl(true)
  });

  constructor(
    override dialogRef: MatDialogRef<CreateEventDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public userId: number,
    private readonly eventService: EventService,
    private readonly authService: AuthenticationService,
    private readonly router: Router,
    private readonly notification: NotificationService
  ) {
    super();
  }

  ngOnInit() {
    this.formSubmit$
      .pipe(
        withLatestFrom(this.createEventForm.valueChanges.pipe(startWith({}))),
        map(([, eventValue]) => eventValue as EventValue),
        filter((value) => {
          if (!(!!value && !!value.eventName && !!value.startTime)) {
            return false;
          }

          if (!!value.endTime && value.startTime > value.endTime){
            this.notification.showError("Th???i gian kh??ng h???p l???");
            return false;
          }

          return true;
        }),
        takeUntil(this.destroyed$)
      )
      .subscribe((eventValue) => {
        this.eventService
          .createUserEvent({
            eventName: eventValue.eventName,
            startTime: eventValue.startTime,
            endTime: eventValue.endTime,
            allDay: eventValue.allDay,
            userId: this.userId,
            eventType: EventType.Default
          } as CreateEventReq)
          .subscribe((res) => {
            this.confirm();
            this.notification.showSuccess('Th??m s??? ki???n th??nh c??ng');
          });
      });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
