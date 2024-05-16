import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient) { }


  getUserPublicContent() {
  return  this.http.request('post',`${environment.apiUrl}connect/userinfo`, {
      withCredentials: true,
      responseType : "text"
    })
  }

  getAdminPublicContent() {
    return  this.http.request('get','http://localhost:8086/api/v1/admin/resource', {
      withCredentials: true,
      responseType : "text"
    })
  }
}
