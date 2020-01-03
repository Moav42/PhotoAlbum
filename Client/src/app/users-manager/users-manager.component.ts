import { Component, OnInit } from '@angular/core';
import { UserVM } from '../Shared/Models/User';
import { UserManagerService } from '../Shared/Services/user-manager.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-users-manager',
  templateUrl: './users-manager.component.html',
  styleUrls: ['./users-manager.component.css']
})
export class UsersManagerComponent implements OnInit {
  user: UserVM = new UserVM();
  users: UserVM[];
  tableMode: boolean = true;
  
  constructor(private service: UserManagerService, private http: HttpClient) { }


  ngOnInit() {
    this.loaudUsers();
  }


  loaudUsers(){
    this.service.getUsers().subscribe((date: UserVM[]) => this.users = date);

  }

  save(){
    if(this.user.userId == null){
       return
    }
    else{
      this.service.updateUser(this.user).subscribe(date => this.loaudUsers())
    }
    this.cancel();
  }

  editTag(item: UserVM){
    this.user = item;
  }

  cancel(){
    this.user = new UserVM();
    this.tableMode = true;
  }

  delete(item: string){
    this.service.deleteUser(item).subscribe(date => this.loaudUsers());
  }
 
  
}
