import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Role } from 'src/app/common/constant';
import { User } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/model-services/user.service';

@Component({
  selector: 'app-staffs-profile',
  templateUrl: './staffs-profile.component.html',
  styleUrls: ['./staffs-profile.component.scss']
})
export class StaffsProfileComponent implements OnInit, OnDestroy {
  userId = 0;
  user = {} as User;
  role = Role;
  routeSub = new Subscription();

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly userService: UserService
  ) { 
    this.routeSub = this.activatedRoute.params.subscribe(params => {
      console.log(params);
      this.userId = params['id'];
    })
  }

  ngOnInit(): void {
    this.userService.getUserById(this.userId).subscribe(res => {
      this.user = res;
    }) 
  }

  ngOnDestroy(): void {
      this.routeSub.unsubscribe();
  }

}
