import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class authGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: import("@angular/router").ActivatedRouteSnapshot,
    state: import("@angular/router").RouterStateSnapshot): 
    boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    
    return this.authService.AuthenticatedUser$.pipe(
      take(1),
      map(user => {
        const { roles } = route.data;
        if ( !user?.role === roles  ) {
          // User is not authenticated or does not have the required role
          return this.router.createUrlTree(['/forbidden']);
        } else {
          // User is authenticated and has the required role
          return true;
        }
      })
    );
  }
}
