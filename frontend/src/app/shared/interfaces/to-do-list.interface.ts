import { ToDo } from './to-do.interface';

export interface ToDoList {
  id: string;
  name: string;
  ownerUserId?: string;
  toDos: ToDo[];
}
