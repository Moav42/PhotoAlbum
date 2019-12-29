import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselComponent } from './Markup/carousel/carousel.component';
import { HeaderComponent } from './Markup/header/header.component';
import { FooterComponent } from './Markup/footer/footer.component';
import { ElementComponent } from './Markup/element/element.component';
import { TagComponent } from './tag/tag.component';


@NgModule({
  declarations: [
    AppComponent, CarouselComponent, HeaderComponent, FooterComponent, ElementComponent, TagComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
