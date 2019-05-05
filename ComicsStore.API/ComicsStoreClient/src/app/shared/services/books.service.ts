import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandleService } from './error-handle.service';
import { Constants } from '../../../app/app.constants';
import { IBook } from '../models/book';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor(
    private http: HttpClient,
    private errorHandleService: ErrorHandleService) { }

    getBooks(): Observable<any[]> {
      const uri = decodeURIComponent(
        `${Constants.locationAPIUrl}/books`
      );
      return this.http.get<any[]>(uri)
        .pipe(
          tap(_ => console.log('fetched books')),
          catchError(this.errorHandleService.handleError('getBooks', []))
        );
    }

    getBook(id: number): Observable<IBook> {
      const uri = decodeURIComponent(
        `${Constants.locationAPIUrl}/books/${id}`
      );
      return this.http.get<IBook>(uri)
        .pipe(
          tap(_ => console.log('fetched book')),
          catchError(this.errorHandleService.handleError('getBook', null))
        );
    }
}
