import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CanActivateLoginSignUpRoute {
  private authService = inject(AuthService);
  private router = inject(Router);
  canActivate() {
    if (this.authService.isAuthenticated() === false) return true;
    this.router.navigate(['to-do-lists']);
    return false;
  }
}
