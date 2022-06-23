import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { FullCalendarModule } from '@fullcalendar/angular';
import { ReactiveFormsModule } from '@angular/forms'; 

import { AppComponent } from './app.component';
import { AuthComponent } from './components/layouts/auth/auth.component';
import { MainComponent } from './components/layouts/main/main.component';
import { HeaderComponent } from './components/main/header/header.component';
import { FooterComponent } from './components/main/footer/footer.component';
import { MessagesComponent } from './components/main/header/messages/messages.component';
import { NotificationsComponent } from './components/main/header/notifications/notifications.component';
import { MenuSidebarComponent } from './components/main/menu-sidebar/menu-sidebar.component';
import { SearchComponent } from './components/main/header/search/search.component';
import { UserPanelComponent } from './components/main/menu-sidebar/user-panel/user-panel.component';
import { HomePageComponent } from './components/main/home-page/home-page.component';
import { StaffsListComponent } from './components/main/users/staffs/staffs-list/staffs-list.component';
import { CalendarComponent } from './components/main/users/calendar/calendar.component';
import { StaffsCalendarComponent } from './components/main/users/staffs/staffs-calendar/staffs-calendar.component';
import { SigninComponent } from './components/auth/signin/signin.component';
import { ForgotPasswordComponent } from './components/auth/forgot-password/forgot-password.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorHandlerService } from './services/error-handler.service';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    MainComponent,
    HeaderComponent,
    FooterComponent,
    MessagesComponent,
    NotificationsComponent,
    MenuSidebarComponent,
    SearchComponent,
    UserPanelComponent,
    HomePageComponent,
    StaffsListComponent,
    CalendarComponent,
    StaffsCalendarComponent,
    SigninComponent,
    ForgotPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    FullCalendarModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
