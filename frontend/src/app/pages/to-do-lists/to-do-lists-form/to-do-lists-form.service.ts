import { Injectable, inject } from '@angular/core';
import { ToDoList } from '../../../shared/interfaces/to-do-list.interface';
import { HttpService } from '../../../shared/services/http.service';
import { UpdateToDoList } from '../../../shared/interfaces/update-to-do-list.interface';

@Injectable({
  providedIn: 'root',
})
export class ToDoListsFormService {
  private httpService = inject(HttpService);

  public create(toDoList: ToDoList) {
    return this.httpService.post('/ToDoList', toDoList);
  }

  public update(toDoList: UpdateToDoList) {
    return this.httpService.put('/ToDoList', toDoList);
  }

  public getById(id: string) {
    return this.httpService.get<ToDoList>('/ToDoList/' + id);
  }

  public delete(id: string) {
    return this.httpService.delete('/ToDoList/' + id);
  }
}
