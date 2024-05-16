import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/authentification/_services/auth.service';
import { StorageService } from 'src/app/authentification/_services/storage.service';

@Component({
  selector: 'app-access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./access-denied.component.scss']
})
export class AccessDeniedComponent {

  constructor(private storageService : StorageService,private authService : AuthService,private router : Router) {
  }

  goHome(): void {
    this.router.navigate(['/home']);
    console.log(this.authService.AuthenticatedUser$)
    // console.log(this.storageService.getToken())
  }

  logout(): void {
    this.authService.logout();
  }
}
