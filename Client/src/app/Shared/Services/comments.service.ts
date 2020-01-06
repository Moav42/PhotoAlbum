import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  private url = "https://localhost:44380/api/Comments";
  
  constructor(private http: HttpClient) { }

  addComment(commentModel:Comment){
    return this.http.post(this.url, commentModel)
  }
}
