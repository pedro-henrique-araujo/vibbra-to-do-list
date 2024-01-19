import { ToDo } from './to-do.interface';

export interface UpdateToDoList {
  id?: string;
  name?: string;
  toDos?: ToDo[];
  deletedToDosIds?: string[];
}
