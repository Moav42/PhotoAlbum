import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Organisation } from '../Models/Organisation';

@Injectable({
  providedIn: 'root'
})
export class OrganisationsManagerService {

  private url = "https://localhost:44380/api/AccountManager/organisation";

  constructor(private http: HttpClient) { }

  getOrgs(){
    return this.http.get(this.url)
  }

  updateOrg(org: Organisation){
    return this.http.put(this.url + '/edit/' + org.id, org)
  }
  deleteOrg(id: number){
    return this.http.delete(this.url + '/delete/' + id)
  }
    
  
}
