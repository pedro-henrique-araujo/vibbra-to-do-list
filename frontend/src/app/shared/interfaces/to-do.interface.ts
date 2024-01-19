export interface ToDo {
  id?: string;
  name?: string;
  isDone?: boolean;
  index?: number;
  children?: ToDo[];
}
