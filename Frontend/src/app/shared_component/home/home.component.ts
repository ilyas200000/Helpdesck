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
          // If user is authenticated, redirect to home page
          this.router.navigate(['/home']);
        } else {
          // Fetch public content only if user is not authenticated
          this.userService.getUserPublicContent().subscribe({
            next: data => {
              this.pubContent = data;
            },
            error: err => console.log(err)
          });
        }
      }
    });
  }


  ngOnDestroy() {
    this.AuthUserSub.unsubscribe();
  }
}
