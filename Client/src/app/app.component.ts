import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from './Shared/Models/UserAccount';
import { Router } from '@angular/router';
import { AuthenticationService } from './Shared/Services';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  ngOnInit() { }
  title = 'RDA';
  
  currentUser: User;



}
