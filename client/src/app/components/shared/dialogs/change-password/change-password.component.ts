import { Component, OnInit } from '@angular/core';
import { DialogComponent } from '../dialog.component';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent extends DialogComponent implements OnInit {

  constructor() { 
    super();
  }

  ngOnInit(): void {
  }

}
