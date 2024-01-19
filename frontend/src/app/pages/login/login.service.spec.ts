import { TestBed } from '@angular/core/testing';
import { AuthService } from '../../shared/services/auth.service';
import { HttpService } from '../../shared/services/http.service';
import { LoginService } from './login.service';
import { emptyUserId } from '../../shared/utils/empty-user-id';
import { asyncData } from '../../shared/utils/async-data';

describe(LoginService.name, () => {
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let httpServiceSpy: jasmine.SpyObj<HttpService>;
  let loginService: LoginService;
  beforeEach(async () => {
    const authSpyValue = jasmine.createSpyObj('AuthService', ['authorize']);
    const httpSpyValue = jasmine.createSpyObj('HttpService', ['get']);
    await TestBed.configureTestingModule({
      providers: [
        LoginService,
        { provide: AuthService, useValue: authSpyValue },
        { provide: HttpService, useValue: httpSpyValue },
      ],
    }).compileComponents();

    loginService = TestBed.inject(LoginService);
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    httpServiceSpy = TestBed.inject(HttpService) as jasmine.SpyObj<HttpService>;
  });

  it(
    LoginService.prototype.login.name +
      ' when user is does not exist return true',
    (done) => {
      const user = { id: emptyUserId, userName: 'abc' };
      httpServiceSpy.get
        .withArgs('/User/' + user.userName)
        .and.returnValue(asyncData(user));
      loginService.login(user.userName).subscribe((invalid) => {
        expect(invalid).toBeTrue();
        done();
      });
    }
  );

  it(
    LoginService.prototype.login.name +
      ' when user exists call authorize and return false',
    (done) => {
      const user = { id: '123', userName: 'abc' };
      httpServiceSpy.get
        .withArgs('/User/' + user.userName)
        .and.returnValue(asyncData(user));
      loginService.login(user.userName).subscribe((invalid) => {
        expect(invalid).toBeFalse();
        expect(authServiceSpy.authorize.calls.mostRecent().args).toEqual([
          user,
        ]);
        done();
      });
    }
  );
});
