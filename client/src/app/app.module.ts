import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AuthComponent } from './components/layouts/auth/auth.component';
import { MainComponent } from './components/layouts/main/main.component';
import { HeaderComponent } from './components/main/header/header.component';
import { FooterComponent } from './components/main/footer/footer.component';
import { MessagesComponent } from './components/main/header/messages/messages.component';
import { NotificationsComponent } from './components/main/header/notifications/notifications.component';
import { MenuSidebarComponent } from './components/main/menu-sidebar/menu-sidebar.component';
import { MenuItemComponent } from './components/shared/menu-item/menu-item.component';
import { SearchComponent } from './components/main/header/search/search.component';
import { UserPanelComponent } from './components/main/menu-sidebar/user-panel/user-panel.component';



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
    MenuItemComponent,
    SearchComponent,
    UserPanelComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
