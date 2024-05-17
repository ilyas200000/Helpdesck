import { Injectable } from '@angular/core';
import { User } from '../_models/user.model';

const USER_KEY = 'authenticated-user';
const TOKEN_KEY = 'access-token';

export interface AuthResponseData {
  access_token: string;
  expires_in: number;
  token_type: string;
  scope: string;
}

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  saveUser(user: User): void {
    try {
      window.localStorage.removeItem(USER_KEY);
      window.localStorage.setItem(USER_KEY, JSON.stringify(user));
    } catch (error) {
      console.error('Error saving user to localStorage', error);
    }
  }

  getSavedUser(): User | null {
    try {
      const user = window.localStorage.getItem(USER_KEY);
      if (user) {
        return JSON.parse(user);
      }
      return null;
    } catch (error) {
      console.error('Error retrieving user from localStorage', error);
      return null;
    }
  }

  saveAuthResponseData(authResponseData: AuthResponseData): void {
    try {
      window.localStorage.removeItem(TOKEN_KEY);
      window.localStorage.setItem(TOKEN_KEY, JSON.stringify(authResponseData));
    } catch (error) {
      console.error('Error saving auth response data to localStorage', error);
    }
  }

  getToken(): string | null {
    try {
      const authResponseData = window.localStorage.getItem(TOKEN_KEY);
      if (authResponseData) {
        const parsedData: AuthResponseData = JSON.parse(authResponseData);
        return parsedData.access_token;
      }
      return null;
    } catch (error) {
      console.error('Error retrieving token from localStorage', error);
      return null;
    }
  }

  clean(): void {
    try {
      window.localStorage.removeItem(USER_KEY);
      window.localStorage.removeItem(TOKEN_KEY);
    } catch (error) {
      console.error('Error clearing localStorage', error);
    }
  }
}
