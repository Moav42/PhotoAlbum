import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbSlideEvent, NgbSlideEventSource } from '@ng-bootstrap/ng-bootstrap';
import { NgbCarousel } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {
  @ViewChild('ngcarousel', { static: true }) ngCarousel: NgbCarousel;
  ngOnInit() { }

  title = 'RDA';

  slideActivate(ngbSlideEvent: NgbSlideEvent) {
    console.log(ngbSlideEvent.source);
    console.log(ngbSlideEvent.paused);
    console.log(NgbSlideEventSource.INDICATOR);
    console.log(NgbSlideEventSource.ARROW_LEFT);
    console.log(NgbSlideEventSource.ARROW_RIGHT);
  }

    // Move to specific slide
    navigateToSlide(item) {
      this.ngCarousel.select(item);
      console.log(item)
    }
  
    // Move to previous slide
    getToPrev() {
      this.ngCarousel.prev();
    }
  
    // Move to next slide
    goToNext() {
      this.ngCarousel.next();
    }
  
    // Pause slide
    stopCarousel() {
      this.ngCarousel.pause();
    }
  
    // Restart carousel
    restartCarousel() {
      this.ngCarousel.cycle();
    }
}
