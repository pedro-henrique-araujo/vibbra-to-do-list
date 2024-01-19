import { Component, inject } from '@angular/core';
import { AuthService } from './shared/services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  private authService = inject(AuthService);
  public logOut() {
    this.authService.logOut();
  }
  public isAuthenticated() {
    return this.authService.isAuthenticated();
  }
}
