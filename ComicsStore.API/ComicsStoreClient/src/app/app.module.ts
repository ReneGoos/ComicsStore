import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { BookListComponent } from './books/book-list.components';
import { StoryListComponent } from './stories/story-list.components';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    StoryListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
