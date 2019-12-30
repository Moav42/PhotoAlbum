import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../Models/Category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private url = "https://localhost:44380/api/categories";
  constructor(private http: HttpClient ) { }

  getCategories(){
    return this.http.get(this.url)
  }

  createCategory(item: Category){
    return this.http.post(this.url, item)
  }

  updateCategory(item: Category){
    return this.http.put(this.url + '/' + item.id, item)
  }

  deleteCategory(item: number){
    return this.http.delete(this.url + '/' + item)
  }
}
