import { Component, OnInit } from '@angular/core';
import { FormBuilder } from "@angular/forms";
import { HttpClient } from '@angular/common/http';
import { Tag } from '../Shared/Models/Tag';
import { TagsService } from '../Shared/Services/tags.service';
import { PostService } from '../Shared/Services/post.service';
import { Post } from '../Shared/Models/Post';
import { Category } from '../Shared/Models/Category';
import { CategoryService } from '../Shared/Services/category.service';
import { AlertService } from '../alert';


@Component({
  selector: 'app-moderator',
  templateUrl: './moderator.component.html',
  styleUrls: ['./moderator.component.css']
})

export class ModeratorComponent implements OnInit {

  selectedPostIdForTag: number;
  selectedPostIdForCategory: number;
  selectedPostIdForDelete: number;
  selectedTagId: number;
  selectedCategoryId: number;
  tags: Tag[];
  posts: Post[];
  categories: Category[];
  
  constructor(public fb: FormBuilder,
      private tagService: TagsService,
      private postService: PostService,
      private categoriesService: CategoryService,
      private alertService: AlertService
    )  {}

  ngOnInit() {
    this.loaudTags();
    this.loaudPosts();
    this.loaudCategories();
  }
 
  loaudPosts(){
    this.postService.getPosts().subscribe((date: Post[]) => this.posts = date);
  }

  loaudTags(){
    this.tagService.getTags().subscribe((date: Tag[]) => this.tags = date);
  }

  loaudCategories(){
    this.categoriesService.getCategories().subscribe((date: Category[]) => this.categories = date);
  }

  addTagToPost( alertId: string){
    this.tagService.addTagToPost(this.selectedPostIdForTag, this.selectedTagId).subscribe(error => console.log(error));;
    this.alertService.success('Tag added to post', alertId);
  }

  addPostToCategory(alertId: string){
    this.categoriesService.addPostToCategory(this.selectedPostIdForCategory, this.selectedCategoryId).subscribe(error => console.log(error));;
    this.alertService.success('Post added to category', alertId);
  }
  
  deletePost(alertId: string){
    this.postService.deletePost(this.selectedPostIdForDelete).subscribe(date => console.log(date));
    this.alertService.error("Post deleted", alertId);
  }
}
