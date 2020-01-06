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
  getPost(id:number){
    return this.http.get(this.url+ '/' + id)
  }

  createPost(item: Post){
    return this.http.post(this.url, item)
  }

  updatePost(item: Post){
    return this.http.put(this.url + '/' + item.id, item)
  }

  deletePost(id: number){
    return this.http.delete(this.url + '/' + id)
  }
  
  getTags(id:number){
    return this.http.get(this.url+ '/' + id + '/tags')
  }

  getComments(id:number){
    return this.http.get(this.url+ '/' + id + '/comments')
  }

  postLike(userId: string, postId: number, licked: boolean){
    return this.http.post(this.url+`/${postId}/like`, {userId, postId, licked});
  }

  getPostRate(postId: number, userId: string){
    return this.http.post(this.url+`/${postId}/rate`, {userId, postId});
  }

}
