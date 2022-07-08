import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject, withLatestFrom, startWith, map, filter, takeUntil, tap } from 'rxjs';
import { EventValue } from 'src/app/models/event/event-value';
import { Event } from 'src/app/models/event/event.model'
import { EventService } from 'src/app/services/model-services/event.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-update-event-dialog',
  templateUrl: './update-event-dialog.component.html',
  styleUrls: ['./update-event-dialog.component.scss']
})
export class UpdateEventDialogComponent extends DialogComponent implements OnInit {
  formSubmit$ = new Subject<void>();
  destroyed$ = new Subject<void>();
  event!: Event;

  updateEventForm = new FormGroup({
    eventName: new FormControl(''),
    startTime: new FormControl(''),
    endTime: new FormControl(''),
    allDay: new FormControl(true)
  });

  constructor(
    override dialogRef: MatDialogRef<UpdateEventDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EventValue,
    private readonly eventService: EventService,
    private readonly notification: NotificationService
  ) {
    super();

    this.eventService.getEvent(this.data.id).subscribe(res => {
      this.event = res;
      this.updateEventForm.patchValue({
        eventName: res.eventName,
        startTime: res.startTime,
        endTime: res.endTime,
        allDay: res.allDay
      })
    })
  }

  ngOnInit() {
    this.formSubmit$
      .pipe(
        withLatestFrom(this.updateEventForm.valueChanges.pipe(startWith({}))),
        map(([, eventValue]) => eventValue as EventValue),
        filter((value) => {
          if (!(!!value && !!value.eventName && !!value.startTime)) {
            return false;
          }

          if (!!value.endTime && value.startTime > value.endTime){
            this.notification.showError("Thời gian không hợp lệ");
            return false;
          }

          return true;
        }),
        tap(value => {
          value.id = this.data.id;
        }),
        takeUntil(this.destroyed$)
      )
      .subscribe((eventValue) => {
        this.eventService
          .updateEvent({
            id: this.event.id,
            eventName: eventValue.eventName,
            startTime: eventValue.startTime,
            endTime: eventValue.endTime,
            allDay: eventValue.allDay,
            eventType: this.event.eventType
          } as Event)
          .subscribe((res) => {
            this.notification.showSuccess('Chỉnh sửa sự kiện thành công');
            this.confirm();
          });
      });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
