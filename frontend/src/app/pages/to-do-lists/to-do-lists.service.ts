import { Injectable, inject } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { HttpParams } from '@angular/common/http';
import { Page } from '../../shared/interfaces/page.interface';
import { ToDoList } from '../../shared/interfaces/to-do-list.interface';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ToDoListsService {
  private httpService = inject(HttpService);

  private pageNumber = 1;
  private total = 0;
  private pageSize = 10;

  public getPage() {
    const httpParams = new HttpParams()
      .set('skip', this.getSkip())
      .set('take', 10);
    return this.httpService.get<Page<ToDoList>>('/ToDoList', httpParams).pipe(
      map((page) => {
        this.total = page.total;
        return page.items;
      })
    );
  }

  public getNextPage() {
    this.loadNextPageNumber();
    return this.getPage();
  }

  public getPreviousPage() {
    this.loadPreviousPageNumber();
    return this.getPage();
  }

  public getCurrentPageNumber() {
    return this.pageNumber;
  }

  public getTotalNumberOfPages() {
    return Math.ceil(this.total / this.pageSize);
  }

  private getSkip() {
    return (this.pageNumber - 1) * this.pageSize;
  }

  private loadNextPageNumber() {
    this.pageNumber = Math.min(
      this.pageNumber + 1,
      this.getTotalNumberOfPages()
    );
  }

  private loadPreviousPageNumber() {
    this.pageNumber = Math.min(
      Math.max(this.pageNumber - 1, 1),
      this.getTotalNumberOfPages()
    );
  }
}
