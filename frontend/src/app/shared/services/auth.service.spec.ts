import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { TestBed } from '@angular/core/testing';
import { emptyUserId } from '../utils/empty-user-id';
import { User } from '../interfaces/user.interface';

describe(AuthService.name, () => {
  let authService: AuthService;
  let routerSpy: jasmine.SpyObj<Router>;
  beforeEach(async () => {
    const routerSpyValue = jasmine.createSpyObj('Router', ['navigate']);
    await TestBed.configureTestingModule({
      providers: [
        AuthService,
        {
          provide: Router,
          useValue: routerSpyValue,
        },
        {
          provide: ActivatedRoute,
          useValue: { snapshot: { queryParams: {} } },
        },
      ],
    }).compileComponents();

    authService = TestBed.inject(AuthService);
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  it(
    AuthService.prototype.authorize.name +
      ' should save user on local storage and navigate to to-do-lists',
    () => {
      const user = {
        id: '123',
        userName: 'abc',
      };

      authService.authorize(user);
      const userInLocalStorage = JSON.parse(
        localStorage.getItem('user') ?? '{}'
      ) as User;

      expect(userInLocalStorage)
        .withContext('expect local storage to contain user')
        .toEqual(user);

      expect(routerSpy.navigate.calls.mostRecent().args[0])
        .withContext('expect navigate to to-do-lists to have been called')
        .toEqual(['to-do-lists']);
    }
  );

  it(
    AuthService.prototype.isAuthenticated.name + ' when authorized return true',
    () => {
      localStorage.setItem('user', JSON.stringify({ id: '123' }));
      const isAuthenticated = authService.isAuthenticated();
      expect(isAuthenticated).toBeTrue();
    }
  );

  it(
    AuthService.prototype.isAuthenticated.name +
      ' when invalid or unexisting user return false',
    () => {
      for (const user of [{ id: emptyUserId }, {}]) {
        localStorage.setItem('user', JSON.stringify(user));
        const isAuthenticated = authService.isAuthenticated();
        expect(isAuthenticated).toBeFalse();
      }
    }
  );

  it(
    AuthService.prototype.logOut.name +
      ' should clear local storage and call navigate to root',
    () => {
      localStorage.setItem('user', 'abc');
      authService.logOut();
      expect(localStorage.getItem('user'))
        .withContext('expect local storage to be clear')
        .toBeNull();
      expect(routerSpy.navigate.calls.mostRecent().args[0])
        .withContext('expect navigate call to point to root')
        .toEqual(['']);
    }
  );
});
