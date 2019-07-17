import { Component, OnInit } from '@angular/core';
import { IStory } from '../shared/models/story';
import { StoriesService } from '../shared/services/stories.service';

@Component({
  selector: 'app-stories',
  templateUrl: './story-list.components.html',
  styleUrls: ['./story-list.components.css']
})
export class StoryListComponent implements OnInit {
  pageTitle = 'Story List';

  filterStories: IStory[];
  stories: IStory[];

  listFilter: string;
  errorMessage: string;

  get ListFilter(): string {
    return this.listFilter;
  }

  set ListFilter(value: string) {
    if (this.stories) {
      this.listFilter = value;
      this.filterStories = this.ListFilter ? this.performFilter(value) : this.stories;
    }
  }

  constructor(
    private storiesService: StoriesService) {
    this.ListFilter = '';
  }

  async ngOnInit(): Promise<void> {
    this.storiesService.getStories().subscribe(
      stories => {
        this.stories = stories;
        this.filterStories = this.ListFilter ? this.performFilter(this.ListFilter) : this.stories;
      },
      error => this.errorMessage = <any>error
    );
    //await this.getStories();
  }

  performFilter(filterBy: string): IStory[] {
    console.log(filterBy);
    filterBy = filterBy.toLocaleLowerCase();
    return this.stories.filter((story: IStory) => story.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  async getStories() {
    const promise = new Promise((resolve, reject) => {
      this.storiesService.getStories()
        .toPromise()
        .then(
          res => { // Success
            this.stories = res;
            resolve();
          },
          err => {
            console.error(err);
            this.errorMessage = err;
            reject(err);
          }
        );
    });
    await promise;
  }
}
