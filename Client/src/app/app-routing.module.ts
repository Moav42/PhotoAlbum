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
import { RegisterComponent } from './register/register.component';
import { Role } from './Shared/Models/role';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ModeratorComponent } from './moderator/moderator.component';


const routes: Routes = [
 	
   { path: '', component: PostComponent }
  ,{ path: 'tags', component: TagComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin, Role.Moderator, Role.Organisation] } }
  ,{ path: 'categories', component: CategoryComponent, canActivate: [AuthGuard] }
  ,{ path: 'postsForm', component: PostFormComponent, canActivate: [AuthGuard] }
  ,{ path: 'categories/posts/:id', component: CarouselComponent, canActivate: [AuthGuard] }
  ,{ path: 'posts/:id', component: PostDetailsComponent , canActivate: [AuthGuard]}
  ,{ path: 'admin/createAccount', component: AccountCreationFormComponent, canActivate: [AuthGuard],  data: { roles: [Role.Admin] } }
  ,{ path: 'admin/users', component: UsersManagerComponent, canActivate: [AuthGuard],  data: { roles: [Role.Admin] }  }
  ,{ path: 'admin/organisations', component: OrganisationsManagerComponent, canActivate: [AuthGuard],  data: { roles: [Role.Admin] } }
  ,{ path: 'login', component: LoginComponent }
  ,{ path: 'registration', component: RegisterComponent }
  ,{ path: 'user/changePas', component: ChangePasswordComponent,  canActivate: [AuthGuard] }
  ,{ path: 'moderator', component: ModeratorComponent, canActivate: [AuthGuard],  data: { roles: [Role.Admin, Role.Moderator] }  }
  ,{ path: '**', redirectTo: '/' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
