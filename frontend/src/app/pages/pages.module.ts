import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ToDoListsComponent } from './to-do-lists/to-do-lists.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { RouterModule } from '@angular/router';
import { ToDoListsFormComponent } from './to-do-lists/to-do-lists-form/to-do-lists-form.component';
import { TreeModule } from 'primeng/tree';
import { TreeDragDropService } from 'primeng/api';
import { AboutComponent } from './about/about.component';

@NgModule({
  declarations: [
    LoginComponent,
    SignUpComponent,
    AboutComponent,
    ToDoListsComponent,
    ToDoListsFormComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    RouterModule,
    TreeModule,
  ],
  providers: [TreeDragDropService],
})
export class PagesModule {}
