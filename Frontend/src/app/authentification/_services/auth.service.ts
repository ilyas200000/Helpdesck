import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject, catchError, tap, throwError,mergeMap} from "rxjs";
import {User} from "../_models/user.model";
import {StorageService} from "./storage.service";
import {Router} from "@angular/router";
import { environment } from 'environment';




export interface AuthResponseData {
  access_token: string;
  expires_in: number;
  token_type: string;
  scope: string;
}
interface UserInfo {
  sub: string; 
  name: string;
  email: string;
  role: string[]; 
}



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  AuthenticatedUser$  = new BehaviorSubject<User | null>(null);

  constructor(
    private http: HttpClient,
    private storageService: StorageService,
    private router: Router
  ) { }

  


  login(username: string, password: string) {
    const requestBody = `grant_type=password&username=${encodeURIComponent(username)}&password=${encodeURIComponent(password)}`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      'Authorization': 'Basic ' + btoa(`${environment.client_id}:${environment.client_secret}`)
    });
   
  
    return this.http.post<AuthResponseData>(`${environment.apiUrl}connect/token`, requestBody,{ headers }).pipe(
      mergeMap(response => {
        if (response && response.access_token) {
          // Token retrieved, save it or perform further actions
          const token = response.access_token;
          // Example: Save token to localStorage
          localStorage.setItem('access_token', token);
        } else {
          // Handle missing token in response
          throw new Error('Access token not found in response');
        }
  
        // After getting the access token, make another request to get user info
        return this.http.get<UserInfo>(`${environment.apiUrl}connect/userinfo`, {
          headers: {
            Authorization: `Bearer ${response.access_token}`
          }
        });
      }),
      catchError(err => {
        console.log(err);
        let errorMessage = 'An unknown error occurred!';
        if (err.error.message === 'Bad credentials') {
          errorMessage = 'The email address or password you entered is invalid';
        }
        return throwError(() => new Error(errorMessage));
      }),
      tap(user => {
        // Save user information or perform further actions
        this.storageService.saveUser(user);
        this.AuthenticatedUser$.next(user);
      })
    );
  }

  
  
  

  autoLogin() {
    const userData = this.storageService.getSavedUser();
    if (!userData) {
      return;
    }
    this.AuthenticatedUser$.next(userData);
  }

  logout(){
    this.http.request('post','http://localhost:8086/api/v1/auth/logout',{
      withCredentials: true
    }).subscribe({
      next: () => {
        this.storageService.clean();
        this.AuthenticatedUser$.next(null);
        this.router.navigate(['/login']);
      }
    })

  }

  refreshToken(){
    return this.http.request('post', 'http://localhost:8086/api/v1/auth/refresh-token-cookie', {
      withCredentials: true
    })
  }
}
