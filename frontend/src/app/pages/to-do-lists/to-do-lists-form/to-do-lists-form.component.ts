import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoListsFormService } from './to-do-lists-form.service';
import { v4 as uuid } from 'uuid';
import { TreeNode } from 'primeng/api';
import { ToDo } from '../../../shared/interfaces/to-do.interface';
import { AuthService } from '../../../shared/services/auth.service';
import { UITreeNode } from 'primeng/tree';

UITreeNode.prototype.onKeyDown = () => {};

@Component({
  templateUrl: './to-do-lists-form.component.html',
})
export class ToDoListsFormComponent implements OnInit {
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private formBuilder = inject(FormBuilder);
  private toDoListsFormService = inject(ToDoListsFormService);
  private authService = inject(AuthService);
  private id!: string;
  public nameControl = this.formBuilder.control('', Validators.required);
  public editMode = false;
  public treeNodes: TreeNode[] = [
    {
      key: uuid(),
      data: false,
    },
  ];

  public showExclusionModal = false;

  private toDos!: ToDo[];
  private deletedToDosIds: string[] = [];
  private ownerUserId: string | undefined;

  public save() {
    this.mapNodesToToDos();
    if (this.editMode) {
      this.updateList();
      return;
    }
    this.createList();
  }

  private createList() {
    if (!this.nameControl.value) return;
    this.toDoListsFormService
      .create({ id: uuid(), name: this.nameControl.value, toDos: this.toDos })
      .subscribe(this.navigateToListTable);
  }

  private updateList() {
    if (!this.nameControl.value) return;
    this.toDoListsFormService
      .update({
        id: this.id,
        name: this.nameControl.value,
        toDos: this.toDos,
        deletedToDosIds: this.deletedToDosIds,
      })
      .subscribe(this.navigateToListTable);
  }

  public ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.editMode = true;
    this.id = id;
    this.toDoListsFormService.getById(this.id).subscribe((toDoList) => {
      this.nameControl.setValue(toDoList.name);
      this.toDos = toDoList.toDos;
      this.ownerUserId = toDoList.ownerUserId;
      this.mapToDosToNodes();
    });
  }

  public addChild(node: TreeNode) {
    node.children = [{ key: uuid(), data: false }, ...(node.children || [])];
    node.expanded = true;
  }

  public addRootChild() {
    this.treeNodes = [{ key: uuid(), data: false }, ...this.treeNodes];
  }

  public remove(node: TreeNode) {
    let containerList = node.parent?.children ?? this.treeNodes;
    const nodeIndex = containerList.indexOf(node);
    if (nodeIndex == undefined) return;
    containerList.splice(nodeIndex, 1);
    if (!node.key) return;
    this.deletedToDosIds = [node.key, ...this.deletedToDosIds];
  }

  public openExclusionModal() {
    this.showExclusionModal = true;
  }

  public hideExclusionModal() {
    this.showExclusionModal = false;
  }

  public removeList() {
    this.toDoListsFormService
      .delete(this.id)
      .subscribe(this.navigateToListTable);
  }

  public userIsOwner() {
    const userId = this.authService.getUserId();
    return userId == this.ownerUserId;
  }

  private mapNodesToToDos = () => {
    this.toDos = this.treeNodes.map(this.mapToToDo);
  };

  private mapToToDo = (node: TreeNode, index: number): ToDo => {
    return {
      id: node.key,
      name: node.label,
      isDone: node.data,
      index: index,
      children: node.children?.map(this.mapToToDo),
    };
  };

  private mapToDosToNodes = () => {
    this.treeNodes = this.toDos.sort(this.toDoSort).map(this.mapNode);
  };

  private mapNode = (toDo: ToDo): TreeNode => {
    return {
      key: toDo.id,
      label: toDo.name,
      data: toDo.isDone,
      expanded: true,
      children: toDo.children?.sort(this.toDoSort).map(this.mapNode),
    };
  };

  private navigateToListTable = () => {
    this.router.navigate(['to-do-lists']);
  };

  private toDoSort = (current: ToDo, previous: ToDo) => {
    return (current?.index || 0) - (previous?.index || 0);
  };
}
