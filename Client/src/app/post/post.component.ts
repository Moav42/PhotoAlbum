import { Component, OnInit } from '@angular/core';
import { PostService } from '../Shared/Services/post.service';
import { Post } from '../Shared/Models/Post';
import {HttpClient} from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  posts: Post[];
  tableMode: boolean = true;
  id: String;
  public searchString: string = ''
  pageOfItems: Array<any>;

  constructor(private _postService: PostService , private http: HttpClient, private router: Router , public route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.loaudPosts();   
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

  loaudPosts(){
    this._postService.getPosts().subscribe((date: Post[]) => this.posts = date);
  }

  public louadImage = (postId: number) =>{
      return `https://localhost:44380/api/Posts/${postId}/image` 
  }
  
  openPost(event){
    this.router.navigate(['/posts']).then(success => console.log('navigation success?' , success))
    .catch(console.error); 
  }

}
