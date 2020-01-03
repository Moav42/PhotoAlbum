import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TagComponent } from './tag/tag.component';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { CategoryComponent } from './category/category.component';
import { PostComponent } from './post/post.component';
import { PostFormComponent } from './post-form/post-form.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { AccountCreationFormComponent } from './account-creation-form/account-creation-form.component';
import { UsersManagerComponent } from './users-manager/users-manager.component';
import { OrganisationsManagerComponent } from './organisations-manager/organisations-manager.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './helpers';


const routes: Routes = [
 	
   { path: '', component: PostComponent }
  ,{ path: 'tags', component: TagComponent }
  //,{ path: 'categories', component: CategoryComponent , canActivate: [AuthGuard]}
  ,{ path: 'categories', component: CategoryComponent }
  ,{ path: 'postsForm', component: PostFormComponent }
  ,{ path: 'categories/posts/:id', component: CarouselComponent }
  ,{ path: 'posts/:id', component: PostDetailsComponent }
  ,{ path: 'admin/createAccount', component: AccountCreationFormComponent }
  ,{ path: 'admin/users', component: UsersManagerComponent }
  ,{ path: 'admin/organisations', component: OrganisationsManagerComponent }
  ,{ path: 'login', component: LoginComponent }
  ,{ path: '**', redirectTo: '/' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
