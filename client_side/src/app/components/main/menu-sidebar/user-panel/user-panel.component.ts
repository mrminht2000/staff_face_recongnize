import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss'],
})
export class UserPanelComponent implements OnInit {
  constructor(
    public readonly authService: AuthenticationService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {}

  navProfile() {
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => 
      this.router.navigate(['/staffs', this.authService.currentUser.id])
    );
  }
}
