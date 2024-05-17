import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor as AngularHttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, concatMap, switchMap, take, throwError, Observable } from 'rxjs';
import { AuthService } from "../_services/auth.service";
import { Router } from "@angular/router";
import { StorageService } from '../_services/storage.service';

@Injectable()
export class HttpInterceptor implements AngularHttpInterceptor {

  constructor(
    private storageService: StorageService,
    private authService: AuthService,
    private router: Router
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return this.authService.AuthenticatedUser$.pipe(
      take(1),
      switchMap(user => {
        if (!user) {
          return next.handle(request);
        }

        // Clone the request to add the new header
        const clonedRequest = request.clone({
          setHeaders: {
            Authorization: `Bearer ${this.storageService.getToken()}`
          }
        });

        return next.handle(clonedRequest).pipe(
          catchError(err => {
            if (err instanceof HttpErrorResponse) {
              switch (err.status) {
                case 403:
                  this.router.navigate(['forbidden']);
                  break;
                case 401:
                  // Attempt to refresh the token
                  return this.authService.refreshToken(this.storageService.getToken()).pipe(
                    concatMap(() => {
                      // After refreshing the token, retry the failed request
                      const newRequest = request.clone({
                        setHeaders: {
                          Authorization: `Bearer ${this.storageService.getToken()}`
                        }
                      });
                      return next.handle(newRequest);
                    }),
                    catchError(refreshErr => {
                      if (refreshErr.status === 403 || refreshErr.status === 401) {
                        this.authService.logout();
                        this.router.navigate(['/login']);
                      }
                      return throwError(() => refreshErr);
                    })
                  );
              }
            }
            return throwError(() => err);
          })
        );
      })
    );
  }
}
