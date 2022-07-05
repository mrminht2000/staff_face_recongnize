import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Role } from './common/constant';
import { ForgotPasswordComponent } from './components/auth/forgot-password/forgot-password.component';
import { SigninComponent } from './components/auth/signin/signin.component';
import { AuthComponent } from './components/layouts/auth/auth.component';
import { MainComponent } from './components/layouts/main/main.component';
import { HomePageComponent } from './components/main/home-page/home-page.component';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';
import { CalendarComponent } from './components/main/users/calendar/calendar.component';
import { StaffsCalendarComponent } from './components/main/users/staffs/staffs-calendar/staffs-calendar.component';
import { StaffsListComponent } from './components/main/users/staffs/staffs-list/staffs-list.component';
import { StaffsProfileComponent } from './components/main/users/staffs/staffs-profile/staffs-profile.component';

import { AuthGuardService as AuthGuard } from './services/guard-services/auth-guard.service';
import { LoginGuardService as LoginGuard } from './services/guard-services/login-guard.service';
import { UnconfirmedEventsComponent } from './components/main/users/calendar/unconfirmed-events/unconfirmed-events.component';

const routes: Routes = [
  {path: '', component: MainComponent, 
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
    {path: '', component: HomePageComponent},
    {path: 'calendar', data: {expectedRole: Role.Admin}, children: [
      {path: 'all', component: CalendarComponent},
      {path: 'unconfirmed', component: UnconfirmedEventsComponent}
    ]},
    {path: 'staffs', children: [
      {path: 'all', component: StaffsListComponent, data: {expectedRole: Role.Admin}},
      {path: ':id', component:StaffsProfileComponent},
      {path: 'calendar/:id', component: StaffsCalendarComponent}
    ]}
  ]},
  {path: 'auth', component: AuthComponent, 
    canActivate: [LoginGuard],
    canActivateChild: [LoginGuard],
    children: [
    {path: 'signin', component: SigninComponent},
    {path: 'forgot-password', component: ForgotPasswordComponent}
  ]},
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
