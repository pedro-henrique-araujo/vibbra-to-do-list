import { Component, OnInit, inject } from '@angular/core';
import { ToDoList } from '../../shared/interfaces/to-do-list.interface';
import { ToDoListsService } from './to-do-lists.service';
import { environment } from '../../../environments/environment';

@Component({
  templateUrl: './to-do-lists.component.html',
})
export class ToDoListsComponent implements OnInit {
  public toDoLists!: ToDoList[];
  public showSuccessfullCopyMessage: boolean = false;
  private toDoListsService = inject(ToDoListsService);
  public ngOnInit() {
    this.toDoListsService.getPage().subscribe(this.loadIntoToDoLists);
  }

  public pageCurrentNumber() {
    return this.toDoListsService.getCurrentPageNumber();
  }

  public totalNumberOfPages() {
    return this.toDoListsService.getTotalNumberOfPages();
  }

  public isFirstPage() {
    return this.pageCurrentNumber() == 1;
  }

  public isLastPage() {
    return this.pageCurrentNumber() == this.totalNumberOfPages();
  }

  public loadNextPage() {
    this.toDoListsService.getNextPage().subscribe(this.loadIntoToDoLists);
  }

  public loadPreviousPage() {
    this.toDoListsService.getPreviousPage().subscribe(this.loadIntoToDoLists);
  }

  public copyToClipBoard(id: string) {
    navigator.clipboard.writeText(
      environment.frontendBaseUrl + '/to-do-lists/edit/' + id
    );
    this.showSuccessfullCopyMessage = true;
    setTimeout(() => {
      this.hideSuccessfullCopyMessage();
    }, 10000);
  }

  public hideSuccessfullCopyMessage() {
    this.showSuccessfullCopyMessage = false;
  }

  private loadIntoToDoLists = (lists: ToDoList[]) => {
    this.toDoLists = lists;
  };
}
