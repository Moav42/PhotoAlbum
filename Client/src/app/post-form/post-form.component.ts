import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { PostService } from '../Shared/Services/post.service';
import { Post } from '../Shared/Models/Post';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {

  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  post: Post = new Post();

  tableMode: boolean = true;

  constructor(private http: HttpClient, private postService: PostService) { }

  ngOnInit() {
  }

  
  save(){

    this.postService.createPost(this.post)
    this.cancel();
  }


  cancel(){
    this.post = new Post();
    this.tableMode = true;
  }

  add(){
    this.cancel();
    this.tableMode = false;
  }




  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
 
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
 
    this.http.post('https://localhost:44380/api/Posts/uplaodImage', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}