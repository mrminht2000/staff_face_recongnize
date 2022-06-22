import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent } from './components/auth/forgot-password/forgot-password.component';
import { SigninComponent } from './components/auth/signin/signin.component';
import { AuthComponent } from './components/layouts/auth/auth.component';
import { MainComponent } from './components/layouts/main/main.component';
import { HomePageComponent } from './components/main/home-page/home-page.component';
import { CalendarComponent } from './components/main/users/calendar/calendar.component';
import { StaffsCalendarComponent } from './components/main/users/staffs/staffs-calendar/staffs-calendar.component';
import { StaffsListComponent } from './components/main/users/staffs/staffs-list/staffs-list.component';

const routes: Routes = [
  {path: '', component: MainComponent, children: [
    {path: '', component: HomePageComponent},
    {path: 'staffs', component: StaffsListComponent},
    {path: 'staffs-calendar', component: StaffsCalendarComponent},
    {path: 'calendar', component:CalendarComponent}
  ]},
  {path: 'auth', component: AuthComponent, children: [
    {path: 'signin', component: SigninComponent},
    {path: 'forgot-password', component: ForgotPasswordComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
