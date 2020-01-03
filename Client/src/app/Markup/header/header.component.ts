import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Shared/Services';
import { User } from 'src/app/Shared/Models/UserAccount';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  
  currentUser: User;

  constructor( private router: Router, private authenticationService: AuthenticationService) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    signOut() {
      this.authenticationService.logout();
      this.router.navigate(['/login']);
    }
  ngOnInit() {
  }

  openTagsEditor(event){
    this.router.navigate(['/tags']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openCategoriesEditor(event){
    this.router.navigate(['/categories']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openPostsEditor(event){
    this.router.navigate(['/postsForm']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openHomePage(event){
    this.router.navigate(['']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openRegistrationForm(event){
    this.router.navigate(['/registration']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openLoginForm(event){
    this.router.navigate(['/login']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openRegForm(event){
    this.router.navigate(['/admin/createAccount']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openUsersEditor(event){
    this.router.navigate(['/admin/users']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  openOrganisationsEditor(event){
    this.router.navigate(['/admin/organisations']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }
  
}
