import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TagComponent } from './tag/tag.component';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { CategoryComponent } from './category/category.component';
import { PostComponent } from './post/post.component';


const routes: Routes = [
 	
   { path: '', component: PostComponent }
  ,{ path: 'tags', component: TagComponent }
  ,{ path: 'categories', component: CategoryComponent }
  ,{ path: '**', redirectTo: '/' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
