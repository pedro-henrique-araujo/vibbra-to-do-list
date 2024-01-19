import { Injectable, inject } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { User } from '../../shared/interfaces/user.interface';
import { map, of } from 'rxjs';
import { AuthService } from '../../shared/services/auth.service';
import { emptyUserId } from '../../shared/utils/empty-user-id';

@Injectable({
  providedIn: 'root',
})
export class SignUpService {
  private httpService = inject(HttpService);
  private authService = inject(AuthService);

  public signUp(userName: string) {
    this.httpService
      .post('/User', { userName })
      .subscribe(() =>
        this.loadUser(userName).subscribe((user) => this.login(user))
      );
  }

  public userExists(userName: string) {
    return this.loadUser(userName).pipe(map((user) => user.id != emptyUserId));
  }

  private loadUser(userName: string) {
    return this.httpService.get<User>('/User/' + userName);
  }

  private login(user: User) {
    this.authService.authorize(user);
  }
}
