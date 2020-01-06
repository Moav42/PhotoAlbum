import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  private url = "https://localhost:44380/api/Account/reg";

  constructor(private http: HttpClient) { }

  registerUser(email: string, password: string){
    return this.http.post(this.url + '/user', {email, password});
  }

  changePassword(name: string, oldPassword: string, newPassword: string){
    return this.http.put('https://localhost:44380/api/Account/password', {name, oldPassword, newPassword})
  }
}
