import { Injectable, inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CanActivateAuthRouteService {
  private authService = inject(AuthService);
  private router = inject(Router);
  canActivate() {
    if (this.authService.isAuthenticated()) return true;
    this.router.navigate([''], {
      queryParams: { redirectTo: window.location.pathname },
    });
    return false;
  }
}
