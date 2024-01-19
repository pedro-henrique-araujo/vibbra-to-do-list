import { User } from './../interfaces/user.interface';
import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { emptyUserId } from '../utils/empty-user-id';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private router = inject(Router);

  public authorize(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
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
