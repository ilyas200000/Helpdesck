import {Component, OnDestroy, OnInit} from '@angular/core';
import { AuthService } from 'src/app/authentification/_services/auth.service';
import {Subscription} from "rxjs";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy{
  constructor(private authService : AuthService) {
  }
  showAdminBoard = false;
  AuthUserSub! : Subscription;
  ngOnInit(): void {
   this.AuthUserSub = this.authService.AuthenticatedUser$.subscribe({
      next : user => {
        if(user) {
          this.showAdminBoard = user.role.includes('Admin');
          console.log(user.role)
        }
      }
    })
  }

  handleLogout() {
    this.authService.logout();
  }
  ngOnDestroy(): void {
    this.AuthUserSub.unsubscribe();
  }

}
