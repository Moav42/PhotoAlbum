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
import { PostFormComponent } from './post-form/post-form.component';
import { RegistrationFormComponent } from './account/registration-form/registration-form.component';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { PostFilterPipe } from './Shared/Pipes/post-filter-pipe';




@NgModule({
  declarations: [
    AppComponent, 
    CarouselComponent, 
    HeaderComponent, 
    FooterComponent,
    TagComponent, 
    CategoryComponent, 
    PostComponent, 
    PostFormComponent, 
    RegistrationFormComponent, 
    LoginFormComponent, PostDetailsComponent, PostFilterPipe
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
