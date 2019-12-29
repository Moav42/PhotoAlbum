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
}
