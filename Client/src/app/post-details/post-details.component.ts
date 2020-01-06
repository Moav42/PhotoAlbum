import { Component, OnInit } from '@angular/core';
import { Post } from '../Shared/Models/Post';
import { PostService } from '../Shared/Services/post.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Tag } from '../Shared/Models/Tag';
import { Comment } from '../Shared/Models/Comment';
import { AuthenticationService } from '../Shared/Services';
import { User } from '../Shared/Models/UserAccount';
import { delay } from 'rxjs/operators';
import { CommentsService } from '../Shared/Services/comments.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})

export class PostDetailsComponent implements OnInit {

  id: number;
  post: Post;
  tags: Tag[];
  comments: Comment[];
  currentUser: User;
  private sub: any;
  liked: boolean;
  commentText: string;
  
  constructor(private _postService: PostService,
    public route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private http: HttpClient) {      
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

  ngOnInit() {   
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id'];
    });

    this.loaudPost();
    this.loaudTags();
    this.loaudComments();
    this.getPostRate()
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
  
  postLike(){
    this._postService.postLike(this.currentUser.id, this.id, true).subscribe( () => this.getPostRate()); 
  }

  getPostRate(){
    this._postService.getPostRate(this.id, this.currentUser.id).pipe(delay(500)).subscribe((date: boolean) => this.liked = date);
  }
  addComment(){
    let id = 0;
    let postId = this.id;
    let userId = this.currentUser.id;
    let text = this.commentText;
    this.http.post("https://localhost:44380/api/Comments", {id, postId, userId, text}).subscribe(() => this.loaudComments() );
    this.commentText = '';
  }
}
