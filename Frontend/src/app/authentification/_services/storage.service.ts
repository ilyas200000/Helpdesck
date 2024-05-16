import { Injectable } from '@angular/core';
import {User} from "../_models/user.model";

const USER_KEY = 'authenticated-user';
const TOKEN_KEY = 'access-token';

@Injectable({
  providedIn: 'root'
})
export class StorageService {



  constructor() { }


  saveUser(user : User){
    window.localStorage.removeItem(USER_KEY);
    window.localStorage.setItem(USER_KEY, JSON.stringify(user));
  }
  getSavedUser() : User | null {
    const user = window.localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }
    return null;
  }

  saveToken(token: string): void {
    window.localStorage.removeItem(TOKEN_KEY);
    window.localStorage.setItem(TOKEN_KEY, token);
  }

  getToken(): string | null {
    return window.localStorage.getItem(TOKEN_KEY);
  }

  clean(): void {
    window.localStorage.clear();
  }
}
