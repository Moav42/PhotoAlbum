import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserVM } from '../Models/User';


@Injectable({
  providedIn: 'root'
})
export class UserManagerService {

  private url = "https://localhost:44380/api/AccountManager/users";

  constructor(private http: HttpClient) { }

  getUsers(){
    return this.http.get(this.url)
  }

  updateUser(user: UserVM){
    return this.http.put(this.url + '/edit', user)
  }
  deleteUser(userName: string){
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json', 
      }),
      body: {
        userName
      },
    };
    
    return this.http.delete(this.url + '/delete', options)
  }
}
