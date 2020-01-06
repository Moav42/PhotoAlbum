import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { PostService } from '../Shared/Services/post.service';
import { Post } from '../Shared/Models/Post';
import { FormsModule }   from '@angular/forms';
import { AuthenticationService } from '../Shared/Services';
import { User } from '../Shared/Models/UserAccount';
import { AlertService } from '../alert';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {

  fileData: File = null;
  previewUrl:any = null;
  fileUploadProgress: string = null;
  uploadedFilePath: string = null;
  post: Post = new Post();
  currentUser: User;

  constructor(private http: HttpClient, private authenticationService: AuthenticationService, private alertService: AlertService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }
   
  ngOnInit() {
  }
   
  fileProgress(fileInput: any) {
      this.fileData = <File>fileInput.target.files[0];
      this.preview();
  }
 
  preview() {
    var mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    var reader = new FileReader();      
    reader.readAsDataURL(this.fileData); 
    reader.onload = (_event) => { 
      this.previewUrl = reader.result; 
    }
  }
   
  onSubmit() {
    const formData = new FormData();
    formData.append('files', this.fileData);
     
    this.fileUploadProgress = '0%';
 
    this.http.post('https://localhost:44380/api/Posts/uplaodImage', formData, {
      reportProgress: true,
      observe: 'events'   
    })
    .subscribe(events => {
      if(events.type === HttpEventType.UploadProgress) {
        this.fileUploadProgress = Math.round(events.loaded / events.total * 100) + '%';
        console.log(this.fileUploadProgress);
      } else if(events.type === HttpEventType.Response) {
        this.fileUploadProgress = '';
        console.log(events.body);             
      }        
    }) 
    this.post.userId = this.currentUser.id;
    this.post.locationPath = this.fileData.name;
    let id = this.post.id = 0;
    let title = this.post.title;
    let locationPath = "images\\\\"+ this.post.locationPath ;
    let userId = this.post.userId;
    this.http.post('https://localhost:44380/api/Posts', {id, title, locationPath, userId }).subscribe(res => {console.log(res);})
    this.post.title = '';
    this.alertService.success("Post added");
  }
}