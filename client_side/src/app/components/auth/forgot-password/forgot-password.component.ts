import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { filter, map, startWith, Subject, withLatestFrom } from 'rxjs';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  forgotForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
  });
  
  formSubmit$ = new Subject<void>();

  constructor(
  ) {}

  ngOnInit(): void {
    this.formSubmit$
      .pipe(
        withLatestFrom(this.forgotForm.valueChanges.pipe(startWith({}))),
        map(([, email]) => email),
        filter((value) => {
          return value && value.email;
        })
      )
      .subscribe((value) => {
        console.log(value);
      });
  }
}
