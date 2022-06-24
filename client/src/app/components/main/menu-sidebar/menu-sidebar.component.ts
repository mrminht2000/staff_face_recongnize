import { Component } from '@angular/core';
import { Role } from 'src/app/common/constant';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-menu-sidebar',
  templateUrl: './menu-sidebar.component.html',
  styleUrls: ['./menu-sidebar.component.scss']
})
export class MenuSidebarComponent {
  constructor(private readonly authService: AuthenticationService) {}

  isAdmin() {
    return this.authService.currentUser.role >= Role.Admin;
  }

}


