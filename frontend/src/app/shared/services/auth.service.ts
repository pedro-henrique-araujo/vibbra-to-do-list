import { User } from './../interfaces/user.interface';
import { Injectable, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { emptyUserId } from '../utils/empty-user-id';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);

  public authorize(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    const redirectTo = this.activatedRoute.snapshot.queryParams['redirectTo'];
    if (redirectTo) {
      this.router.navigate([redirectTo]);
      return;
    }
    this.router.navigate(['to-do-lists']);
  }

  public isAuthenticated() {
    const id = this.getUserId();
    if (!id || id == emptyUserId) return false;
    return true;
  }

  public logOut() {
    localStorage.clear();
    this.router.navigate(['']);
  }

  public getUserId() {
    const localStorageUserItem = localStorage.getItem('user');
    if (!localStorageUserItem) return false;
    const user = JSON.parse(localStorageUserItem) as User;
    return user.id;
  }
}
