import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NgModule } from '@angular/core';
import { PagesModule } from './pages/pages.module';
import { ToDoListsComponent } from './pages/to-do-lists/to-do-lists.component';
import { CanActivateAuthRouteService } from './shared/services/can-activate-auth-route.service';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { ToDoListsFormComponent } from './pages/to-do-lists/to-do-lists-form/to-do-lists-form.component';
import { AboutComponent } from './pages/about/about.component';
import { CanActivateLoginSignUpRoute } from './shared/services/can-activate-login-sign-up-route.service';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    canActivate: [CanActivateLoginSignUpRoute],
  },

  {
    path: 'sign-up',
    component: SignUpComponent,
    canActivate: [CanActivateLoginSignUpRoute],
  },

  {
    path: 'about',
    component: AboutComponent,
  },

  {
    path: 'to-do-lists',
    component: ToDoListsComponent,
    canActivate: [CanActivateAuthRouteService],
  },

  {
    path: 'to-do-lists/new',
    component: ToDoListsFormComponent,
    canActivate: [CanActivateAuthRouteService],
  },

  {
    path: 'to-do-lists/edit/:id',
    component: ToDoListsFormComponent,
    canActivate: [CanActivateAuthRouteService],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes), PagesModule],
  exports: [RouterModule],
})
export class AppRoutingModule {}
