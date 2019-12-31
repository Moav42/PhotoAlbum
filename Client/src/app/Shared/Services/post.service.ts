import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '../Models/Post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private url = "https://localhost:44380/api/posts";
  constructor(private http: HttpClient) { }

  getPosts(){
    return this.http.get(this.url)
  }
  getImage(path: string){
    return this.http.get("https://localhost:44380/api/images/" + path)
  }

  createPost(item: Post){
    return this.http.post(this.url, item)
  }

  updatePost(item: Post){
    return this.http.put(this.url + '/' + item.id, item)
  }

  deletePost(item: Post){
    return this.http.delete(this.url + '/' + item)
  }
}