import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Tag } from '../Models/Tag';

@Injectable({
  providedIn: 'root'
})
export class TagsService {
  private url = "https://localhost:44380/api/tags";

  constructor(private http: HttpClient) { }

  getTags(){
    return this.http.get(this.url)
  }

  createTag(tag: Tag){
    return this.http.post(this.url, tag)
  }

  updateTag(tag: Tag){
    return this.http.put(this.url + '/' + tag.id, tag)
  }

  deleteTag(id: number){
    return this.http.delete(this.url + '/' + id)
  }

  addTagToPost(postId: number, tagId: number){
    return this.http.post(this.url +'/post', {postId, tagId})
  }
}
