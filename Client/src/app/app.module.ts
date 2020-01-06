import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { HeaderComponent } from './Markup/header/header.component';
import { FooterComponent } from './Markup/footer/footer.component';
import { TagComponent } from './tag/tag.component';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CategoryComponent } from './category/category.component';
import { PostComponent } from './post/post.component';
import { PostFormComponent } from './post-form/post-form.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { PostFilterPipe } from './Shared/Pipes/post-filter-pipe';
import { UsersManagerComponent } from './users-manager/users-manager.component';
import { OrganisationsManagerComponent } from './organisations-manager/organisations-manager.component';
import { AccountCreationFormComponent } from './account-creation-form/account-creation-form.component';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor, ErrorInterceptor } from './helpers';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ModeratorComponent } from './moderator/moderator.component';
import { AlertComponent } from './alert/alert.component';
import { AlertModule } from './alert/alert.module';


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
    PostDetailsComponent, 
    PostFilterPipe,
    UsersManagerComponent,
    OrganisationsManagerComponent, 
    AccountCreationFormComponent, 
    LoginComponent, 
    RegisterComponent,
    JwPaginationComponent,
    ChangePasswordComponent,
    ModeratorComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule, BrowserModule,
    ReactiveFormsModule,
    AlertModule
  ],
  
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
