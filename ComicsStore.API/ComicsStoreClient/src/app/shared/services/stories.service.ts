import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandleService } from './error-handle.service';
import { Constants } from '../../../app/app.constants';
import { IStory } from '../models/story';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoriesService {

  constructor(
    private http: HttpClient,
    private errorHandleService: ErrorHandleService) { }

    getStories(): Observable<IStory[]> {
      const uri = decodeURIComponent(
        `${Constants.locationAPIUrl}/stories`
      );
      return this.http.get<IStory[]>(uri)
        .pipe(
          tap(_ => console.log('fetched stories')),
          catchError(this.errorHandleService.handleError('getStories', []))
        );
    }

    getStory(id: number): Observable<IStory> {
      const uri = decodeURIComponent(
        `${Constants.locationAPIUrl}/stories/${id}`
      );
      return this.http.get<IStory>(uri)
        .pipe(
          tap(_ => console.log('fetched story')),
          catchError(this.errorHandleService.handleError('getStory', null))
        );
    }
}
