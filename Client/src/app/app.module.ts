import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { HeaderComponent } from './Markup/header/header.component';
import { FooterComponent } from './Markup/footer/footer.component';
import { TagComponent } from './tag/tag.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CategoryComponent } from './category/category.component';
import { PostComponent } from './post/post.component';




@NgModule({
  declarations: [
    AppComponent, 
    CarouselComponent, 
    HeaderComponent, 
    FooterComponent,
    TagComponent, 
    CategoryComponent, PostComponent 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule
    
   
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
