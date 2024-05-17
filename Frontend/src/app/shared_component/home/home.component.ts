import {Component, OnDestroy, OnInit} from '@angular/core';
import { UserService } from 'src/app/authentification/_services/user.service';
import { AuthService } from 'src/app/authentification/_services/auth.service';
import { User } from 'src/app/authentification/_models/user.model';
import {Subscription} from "rxjs";
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  pubContent : string = '';
  user! : User;
  AuthUserSub! : Subscription;
  constructor(
    private router: Router,
    private userService : UserService,
    private authService : AuthService
  ) {
  }
  ngOnInit(): void {
    this.AuthUserSub = this.authService.AuthenticatedUser$.subscribe({
      next: user => {
        if (user) {
          this.user = user; // Assign the user value to the user property
        } else {
          // User is not authenticated, navigate to login page
          this.router.navigate(['/login']);
        }
      }
    });
  }
  


  ngOnDestroy() {
    this.AuthUserSub.unsubscribe();
  }
}
