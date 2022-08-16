import { Component, HostListener } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  template: '',
})
export abstract class DialogComponent {
  protected dialogRef!: MatDialogRef<DialogComponent>;

  public cancel() {
    this.close(false);
  }

  public close(value: any) {
    this.dialogRef.close(value);
  }

  public confirm() {
    this.close(true);
  }

  @HostListener('keydown.esc')
  public onEsc() {
    this.close(false);
  }
}
