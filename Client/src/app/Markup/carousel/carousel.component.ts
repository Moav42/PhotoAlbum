import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NgbSlideEvent, NgbSlideEventSource } from '@ng-bootstrap/ng-bootstrap';
import { NgbCarousel } from '@ng-bootstrap/ng-bootstrap';
import { Post } from 'src/app/Shared/Models/Post';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/Shared/Services/category.service';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit, OnDestroy  {
  @ViewChild('ngcarousel', { static: true }) ngCarousel: NgbCarousel;
  @ViewChild('mycarousel', {static : true}) carousel: NgbCarousel;
  posts: Post[];
  tableMode: boolean = true;

  id: number;
  private sub: any;

  constructor(private _categoriesService: CategoryService , private http: HttpClient, private route: ActivatedRoute ) { }

  onSlide(slideEvent: NgbSlideEvent) {
    console.log(slideEvent.source);
    console.log(NgbSlideEventSource.ARROW_LEFT);
    console.log(slideEvent.paused);
    console.log(NgbSlideEventSource.INDICATOR);
    console.log(NgbSlideEventSource.ARROW_RIGHT);
  }
  
  ngOnInit() {
    this.sub = this.route.params.subscribe(params => { this.id = +params['id']; });
    this.loaudPosts();   
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loaudPosts(){
    this._categoriesService.getPostByCategory(this.id).subscribe((date: Post[]) => this.posts = date);
  }

  public louadImage = (postId: number) =>{
      return `https://localhost:44380/api/Posts/${postId}/image` 
  }


  slideActivate(ngbSlideEvent: NgbSlideEvent) {
    console.log(ngbSlideEvent.source);
    console.log(ngbSlideEvent.paused);
    console.log(NgbSlideEventSource.INDICATOR);
    console.log(NgbSlideEventSource.ARROW_LEFT);
    console.log(NgbSlideEventSource.ARROW_RIGHT);
  }

  startCarousel() {
    this.carousel.cycle();
  }
 
  pauseCarousel() {
    this.carousel.pause();
  }
 
  moveNext() {
    this.carousel.next();
  }
 
  getPrev() {
    this.carousel.prev();
  }
 
  goToSlide(slide) {
    this.carousel.select(slide);
  }  
}
