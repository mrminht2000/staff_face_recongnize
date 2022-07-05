import { Injectable } from '@angular/core';
import { ActiveToast, ToastrService } from 'ngx-toastr';
;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private toastr: ToastrService) {}

  inprogressNotification!: ActiveToast<any>;

  showError(message: string, title?: string) {
    this.toastr.error(message,title);
  }

  showSuccess(message: string, title?: string) {
    this.toastr.success(message, title);
  }

  showInfo(message: string, title?: string) {
    this.toastr.info(message, title);
  }

  showInprogress(message: string, title?: string) {
    this.inprogressNotification = this.toastr.info(message, title, {
      disableTimeOut: true,
      progressBar: true
    })
  }

  hideInprogress() {
    this.toastr.clear(this.inprogressNotification.toastId)
  }

  hideAll() {
    this.toastr.clear();
  }
}
