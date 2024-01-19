import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { By } from '@angular/platform-browser';
import { LoginService } from './login.service';
import { ReactiveFormsModule } from '@angular/forms';
import { asyncData } from '../../shared/utils/async-data';

describe(LoginComponent.name, () => {
  let fixture: ComponentFixture<LoginComponent>;
  let component: LoginComponent;
  let loginServiceSpy: jasmine.SpyObj<LoginService>;

  beforeEach(async () => {
    const loginSpyValue = jasmine.createSpyObj(['LoginService', ['login']]);
    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [{ provide: LoginService, useValue: loginSpyValue }],
      imports: [ReactiveFormsModule],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    loginServiceSpy = TestBed.inject(
      LoginService
    ) as jasmine.SpyObj<LoginService>;
  });

  it(
    LoginComponent.prototype.login.name +
      ' when user has no value do not call login',
    () => {
      fixture.detectChanges();
      component.login(new Event('click'));
      expect(loginServiceSpy.login.calls.count()).toBe(0);
    }
  );

  it(
    LoginComponent.prototype.login.name + ' when user is invalid show message',
    fakeAsync(() => {
      fixture.detectChanges();
      loginServiceSpy.login.and.returnValues(asyncData(true));
      component.userNameControl.setValue('abc');
      component.login(new Event('click'));
      tick();
      fixture.detectChanges();
      const element = fixture.debugElement.query(
        By.css('#user-not-exists-validation-message')
      );
      expect(element).toBeTruthy();
    })
  );

  it(
    LoginComponent.prototype.login.name +
      ' when user is valid do not show message',
    fakeAsync(() => {
      fixture.detectChanges();
      loginServiceSpy.login.and.returnValues(asyncData(false));
      component.userNameControl.setValue('abc');
      component.login(new Event('click'));
      tick();
      fixture.detectChanges();
      const element = fixture.debugElement.query(
        By.css('#user-not-exists-validation-message')
      );
      expect(element).toBeFalsy();
    })
  );

  it('should change `userIsInvalid` to false when input changes', () => {
    fixture.detectChanges();
    component.userIsInvalid = true;
    component.userNameControl.setValue('abc');
    fixture.detectChanges();
    expect(component.userIsInvalid).toBeFalse();
  });

  it('should show message when invalid user name input blurs', () => {
    fixture.detectChanges();
    const { nativeElement } = fixture.debugElement.query(
      By.css('input[type=text]')
    );
    nativeElement.dispatchEvent(new Event('blur'));
    fixture.detectChanges();
    const messageElement = fixture.debugElement.query(
      By.css('#required-validation-message')
    );
    expect(messageElement).toBeTruthy();
  });

  it('should disable or enable submit button based on form validation', () => {
    fixture.detectChanges();
    const { nativeElement } = fixture.debugElement.query(By.css('#submit'));
    expect(nativeElement.disabled).toBeTrue();
    component.userIsInvalid = true;
    component.userNameControl.setValue('abc');
    fixture.detectChanges();
    expect(nativeElement.disabled).toBeFalse();
  });
});
