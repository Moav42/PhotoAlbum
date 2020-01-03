import { Component, OnInit } from '@angular/core';
import { Post } from '../Shared/Models/Post';
import { PostService } from '../Shared/Services/post.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Tag } from '../Shared/Models/Tag';
import { Comment } from '../Shared/Models/Comment';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent implements OnInit {
  id: number;
  tableMode: boolean = true;
  post: Post;
  tags: Tag[];
  comments: Comment[];

  private sub: any;


  constructor(private _postService: PostService, private router: Router, public route: ActivatedRoute) { }

  ngOnInit() {
    
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
   
    this.loaudPost();
    this.loaudTags();
    this.loaudComments();
  }
  

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loaudPost(){
    this._postService.getPost(this.id).subscribe((date: Post) => this.post = date);
  }
  
  public louadImage = (postId: number) =>{
      return `https://localhost:44380/api/Posts/${postId}/image` 
  }
  loaudTags(){
    this._postService.getTags(this.id).subscribe((date: Tag[]) => this.tags = date);
  }
  loaudComments(){
    this._postService.getComments(this.id).subscribe((date: Comment[]) => this.comments = date);
  }

}
