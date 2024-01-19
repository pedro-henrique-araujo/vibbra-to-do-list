import { TestBed } from '@angular/core/testing';
import { CanActivateAuthRouteService } from './can-activate-auth-route.service';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
describe(CanActivateAuthRouteService.name, () => {
  let canActivateAuthRouteService: CanActivateAuthRouteService;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerServiceSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    const authSpy = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      providers: [
        CanActivateAuthRouteService,
        { provide: AuthService, useValue: authSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    canActivateAuthRouteService = TestBed.inject(CanActivateAuthRouteService);
    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    routerServiceSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  it(
    CanActivateAuthRouteService.prototype.canActivate.name +
      ' when user is authenticated return true',
    () => {
      authServiceSpy.isAuthenticated.and.returnValue(true);

      const result = canActivateAuthRouteService.canActivate();

      expect(result).toBeTrue();
      expect(routerServiceSpy.navigate.calls.count()).toBe(0);
    }
  );

  it(
    CanActivateAuthRouteService.prototype.canActivate.name +
      ' when user is not authenticated navigate to root and return false',
    () => {
      authServiceSpy.isAuthenticated.and.returnValue(false);
      const result = canActivateAuthRouteService.canActivate();
      expect(result).toBeFalse();
      expect(routerServiceSpy.navigate.calls.mostRecent().args[0]).toEqual([
        '',
      ]);
    }
  );
});
