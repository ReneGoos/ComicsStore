import { Component, OnInit } from '@angular/core';
import { IBook } from '../shared/models/book';
import { BooksService } from '../shared/services/books.service';

@Component({
  selector: 'app-books',
  templateUrl: './book-list.components.html',
  styleUrls: ['./book-list.components.css']
})
export class BookListComponent implements OnInit {
  pageTitle = 'Book List';

  filterBooks: IBook[];
  books: IBook[];

  listFilter: string;
  errorMessage: string;

  get ListFilter(): string {
    return this.listFilter;
  }

  set ListFilter(value: string) {
    if (this.books) {
      this.listFilter = value;
      this.filterBooks = this.ListFilter ? this.performFilter(value) : this.books;
    }
  }

  constructor(
    private booksService: BooksService) {
    this.ListFilter = '';
  }

  async ngOnInit(): Promise<void> {
    await this.getBooks();
  }

  performFilter(filterBy: string): IBook[] {
    console.log(filterBy);
    filterBy = filterBy.toLocaleLowerCase();
    return this.books.filter((book: IBook) => book.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  async getBooks() {
    const promise = new Promise((resolve, reject) => {
      this.booksService.getBooks()
        .toPromise()
        .then(
          res => { // Success
            this.books = res;
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
