import { AuthService } from './../../shared/services/auth.service';
import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpService } from '../../shared/services/http.service';
import { User } from '../../shared/interfaces/user.interface';
import { emptyUserId } from '../../shared/utils/empty-user-id';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private authService = inject(AuthService);
  private httpService = inject(HttpService);
  private user!: User;
  public login(userName: string) {
    return this.httpService.get<User>('/User/' + userName).pipe(
      map((user) => {
        this.user = user;
        if (this.user?.id == emptyUserId) return true;
        this.authorize();
        return false;
      })
    );
  }

  private authorize() {
    this.authService.authorize(this.user);
  }
}
