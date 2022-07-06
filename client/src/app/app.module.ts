import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { FullCalendarModule } from '@fullcalendar/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { CookieModule } from 'ngx-cookie';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { CapitalizePipe } from './common/pipes/capitalize.pipe';

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
import { StaffsProfileComponent } from './components/main/users/staffs/staffs-profile/staffs-profile.component';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';
import { LoadingComponent } from './components/shared/loading/loading.component';
import { NotificationComponent } from './components/shared/notification/notification.component';
import { CreateVacationComponent } from './components/shared/dialogs/create-vacation/create-vacation.component';
import { AuthorizationHeaderService } from './services/intercepts/authorization-header.service';
import { UnconfirmedEventsComponent } from './components/main/users/calendar/unconfirmed-events/unconfirmed-events.component';
import { CreateEventComponent } from './components/shared/dialogs/create-event/create-event.component';

import { ErrorHandlerService } from './services/intercepts/error-handler.service';
import { LoadingHandlerService } from './services/intercepts/loading-handler.service';
import { ConfirmationComponent } from './components/shared/dialogs/confirmation/confirmation.component';
import { UnconfirmedEventsDialogComponent } from './components/shared/dialogs/unconfirmed-events-dialog/unconfirmed-events-dialog.component';
import { ProfileSettingsComponent } from './components/main/users/staffs/staffs-profile/profile-settings/profile-settings.component';
import { EditProfileComponent } from './components/shared/dialogs/edit-profile/edit-profile.component';
import { DashboardComponent } from './components/main/dashboard/dashboard.component';

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
    ForgotPasswordComponent,
    CapitalizePipe,
    StaffsProfileComponent,
    NotFoundComponent,
    LoadingComponent,
    NotificationComponent,
    CreateVacationComponent,
    UnconfirmedEventsComponent,
    CreateEventComponent,
    ConfirmationComponent,
    UnconfirmedEventsDialogComponent,
    ProfileSettingsComponent,
    EditProfileComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    FullCalendarModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CookieModule.forRoot(),
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    ToastrModule.forRoot(),
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationHeaderService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingHandlerService,
      multi: true
    },
    MatDatepickerModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
