import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { BookListComponent } from './books/book-list.components';
import { StoryListComponent } from './stories/story-list.components';
import { AppComponent } from './app.component';
import { BookDetailComponent } from './books/book-detail.component';
import { StoryDetailComponent } from './stories/story-detail.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponent } from './home/welcome.component';
import { ArtistDetailComponent } from './artists/artist-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    StoryListComponent,
    BookDetailComponent,
    StoryDetailComponent,
    WelcomeComponent,
    ArtistDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot(
      [
        { path: 'books', component: BookListComponent },
        { path: 'books/:id', component: BookDetailComponent },
        { path: 'stories', component: StoryListComponent },
        { path: 'stories/:id', component: StoryDetailComponent },
        { path: 'welcome', component: WelcomeComponent },
        { path: '', redirectTo: 'welcome', pathMatch: 'full' },
        { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
      ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
