import { Component, OnInit } from '@angular/core';
import { Category } from '../Shared/Models/Category';
import { CategoryService } from '../Shared/Services/category.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  category: Category = new Category();
  categories: Category[];
  id: String;
  tableMode: boolean = true;
  
  constructor(private service: CategoryService, private router: Router, public route: ActivatedRoute) { 


     this.id = this.route.snapshot.paramMap.get('id');
   }

  ngOnInit() {
    this.loaudCategories();
  }


  loaudCategories(){
    this.service.getCategories().subscribe((date: Category[]) => this.categories = date);
  }
 
  save(){
    if(this.category.id == null){
      this.service.createCategory(this.category).subscribe((date: Category) => this.categories.push(date));
    }
    else{
      this.service.updateCategory(this.category).subscribe(date => this.loaudCategories())
    }
    this.cancel();
  }

  editTag(item: Category){
    this.category = item;
  }

  cancel(){
    this.category = new Category();
    this.tableMode = true;
  }

  delete(item: Category){
    this.service.deleteCategory(item.id).subscribe(date => this.loaudCategories());
  }
  
  add(){
    this.cancel();
    this.tableMode = false;
  }
}
