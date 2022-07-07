import { Component, OnInit } from '@angular/core';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss']
})
export class ChangePasswordDialogComponent extends DialogComponent implements OnInit {

  constructor() { 
    super();
  }

  ngOnInit(): void {
  }

}
