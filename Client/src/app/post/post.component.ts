import { Component, OnInit } from '@angular/core';
import { PostService } from '../Shared/Services/post.service';
import { Post } from '../Shared/Models/Post';

import {HttpClient, HttpParams} from '@angular/common/http';


@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  posts: Post[];
  tableMode: boolean = true;



  constructor(private _postService: PostService , private http: HttpClient ) { }

  ngOnInit() {
    this.loaudPosts();
    
  }

  loaudPosts(){
    this._postService.getPosts().subscribe((date: Post[]) => this.posts = date);
  }
  public louadImage = (postId: number) =>{
      return `https://localhost:44380/api/Posts/image/${postId}`  
  }
  

public createImgPath = (path: string) => {
  //var url  = 
  //return this.http.get(url);
}

}
