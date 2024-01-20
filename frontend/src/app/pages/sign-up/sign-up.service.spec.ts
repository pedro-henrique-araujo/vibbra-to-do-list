import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AuthService } from '../../shared/services/auth.service';
import { HttpService } from '../../shared/services/http.service';
import { SignUpService } from './sign-up.service';
import { asyncData } from '../../shared/utils/async-data';
import { emptyUserId } from '../../shared/utils/empty-user-id';
import { ActivatedRoute } from '@angular/router';

describe(SignUpService.name, () => {
  let signUpService: SignUpService;
  let httpServiceSpy: jasmine.SpyObj<HttpService>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  beforeEach(async () => {
    const httpSpyValue = jasmine.createSpyObj('HttpService', ['get', 'post']);
    const authSpyValue = jasmine.createSpyObj('AuthService', ['authorize']);
    await TestBed.configureTestingModule({
      providers: [
        SignUpService,
        { provide: HttpService, useValue: httpSpyValue },
        { provide: AuthService, useValue: authSpyValue },
      ],
    }).compileComponents();

    signUpService = TestBed.inject(SignUpService);
    httpServiceSpy = TestBed.inject(HttpService) as jasmine.SpyObj<HttpService>;
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
  });

  it(
    SignUpService.prototype.signUp.name + ' call post and authorize',
    fakeAsync(() => {
      const user = { id: '123', userName: 'abc' };

      httpServiceSpy.post
        .withArgs('/User', { userName: user.userName })
        .and.returnValues(asyncData(user));

      httpServiceSpy.get
        .withArgs('/User/' + user.userName)
        .and.returnValue(asyncData(user));
      signUpService.signUp(user.userName);
      tick();
      expect(authServiceSpy.authorize.calls.mostRecent().args[0]).toEqual(user);
    })
  );

  it(
    SignUpService.prototype.userExists.name +
      ' return that user does not exist',
    (done) => {
      const user = { id: emptyUserId, userName: 'abc' };
      httpServiceSpy.get
        .withArgs('/User/' + user.userName)
        .and.returnValue(asyncData(user));

      signUpService.userExists(user.userName).subscribe((exists) => {
        expect(exists).toBeFalse();
        done();
      });
    }
  );

  it(
    SignUpService.prototype.userExists.name + ' return that user exists',
    (done) => {
      const user = { id: '123', userName: 'abc' };
      httpServiceSpy.get
        .withArgs('/User/' + user.userName)
        .and.returnValue(asyncData(user));
      signUpService.userExists(user.userName).subscribe((exists) => {
        expect(exists).toBeTrue();
        done();
      });
    }
  );
});
