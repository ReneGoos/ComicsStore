import { Component, OnInit } from '@angular/core';
import { IStory } from '../shared/models/story';
import { ActivatedRoute, Router } from '@angular/router';
import { StoriesService } from '../shared/services/stories.service';

@Component({
  templateUrl: './story-detail.component.html',
  styleUrls: ['./story-detail.component.css']
})
export class StoryDetailComponent implements OnInit {

  constructor(
    private storiesService: StoriesService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }
  errorMessage: string;
  pageTitle: string = 'Story';
  story: IStory;

  async ngOnInit(): Promise<void> {
    let id = +this.route.snapshot.paramMap.get('id');
    this.storiesService.getStory(id).subscribe(
      story => {
        this.story = story;
      },
      error => this.errorMessage = <any>error
    );
  }

  onBack(): void {
    this.router.navigate(['/stories']);
  }
}
