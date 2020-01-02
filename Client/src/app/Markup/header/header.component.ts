import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( private router: Router) { }

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
}
