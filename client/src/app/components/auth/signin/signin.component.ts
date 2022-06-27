import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { filter, map, startWith, Subject, withLatestFrom, tap } from 'rxjs';
import { AuthenticateParam } from 'src/app/models/auth/authenticate-param';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    remember: new FormControl(false)
  });
  
  formSubmit$ = new Subject<void>();

  constructor( 
    private readonly authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.formSubmit$
      .pipe(
        withLatestFrom(this.loginForm.valueChanges.pipe(startWith({}))),
        map(([, loginValue]) => loginValue as AuthenticateParam),
        filter((value) => {
          return !!value.username || !!value.password;
        }),
        tap((value) => {
          value.password = value.password;
        })
      )
      .subscribe((value) => {
        this.authService.loginUser(value);
      });
  }
}
