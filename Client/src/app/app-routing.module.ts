import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TagComponent } from './tag/tag.component';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { CategoryComponent } from './category/category.component';
import { PostComponent } from './post/post.component';
import { PostFormComponent } from './post-form/post-form.component';
import { RegistrationFormComponent } from './account/registration-form/registration-form.component';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { PostDetailsComponent } from './post-details/post-details.component';


const routes: Routes = [
 	
   { path: '', component: PostComponent }
  ,{ path: 'tags', component: TagComponent }
  ,{ path: 'categories', component: CategoryComponent }
  ,{ path: 'postsForm', component: PostFormComponent }
  ,{ path: 'registration', component: RegistrationFormComponent }
  ,{ path: 'login', component: LoginFormComponent }
  ,{ path: 'categories/posts/:id', component: CarouselComponent }
  ,{ path: 'posts/:id', component: PostDetailsComponent }
  ,{ path: '**', redirectTo: '/' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
