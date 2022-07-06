import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user.model';
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent implements OnInit {
  constructor(
    private readonly dialog: DialogService
  ) { }

  @Input() user!: User;

  ngOnInit(): void {
  }

  openEditProfileDialog() {
    this.dialog.openEditUser(this.user);
  }
}
