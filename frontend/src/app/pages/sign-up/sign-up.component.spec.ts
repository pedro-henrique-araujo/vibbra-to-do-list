import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { SignUpComponent } from './sign-up.component';
import { SignUpService } from './sign-up.service';
import { By } from '@angular/platform-browser';
import { asyncData } from '../../shared/utils/async-data';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';

describe(SignUpComponent.name, () => {
  let component: SignUpComponent;
  let fixture: ComponentFixture<SignUpComponent>;
  let signUpServiceSpy: jasmine.SpyObj<SignUpService>;
  beforeEach(async () => {
    const signUpSpyValue = jasmine.createSpyObj('SignUpService', [
      'signUp',
      'userExists',
    ]);

    await TestBed.configureTestingModule({
      declarations: [SignUpComponent],
      providers: [
        {
          provide: SignUpService,
          useValue: signUpSpyValue,
        },
        {
          provide: ActivatedRoute,
          useValue: { snapshot: { queryParams: {} } },
        },
      ],
      imports: [ReactiveFormsModule, RouterModule],
    }).compileComponents();

    fixture = TestBed.createComponent(SignUpComponent);
    component = fixture.componentInstance;
    signUpServiceSpy = TestBed.inject(
      SignUpService
    ) as jasmine.SpyObj<SignUpService>;
  });

  it(
    SignUpComponent.prototype.signUp.name +
      ' when user has no value do not call userExists',
    () => {
      fixture.detectChanges();
      component.signUp(new Event('click'));
      expect(signUpServiceSpy.userExists.calls.count()).toBe(0);
    }
  );

  it(
    SignUpComponent.prototype.signUp.name + ' when user exists show message',
    fakeAsync(() => {
      fixture.detectChanges();
      component.userNameControl.setValue('abc');

      fixture.detectChanges();
      const submitElement = fixture.debugElement.query(
        By.css('#submit')
      ).nativeElement;
      signUpServiceSpy.userExists.and.returnValue(asyncData(true));
      submitElement.click();
      tick();
      fixture.detectChanges();
      const validationElement = fixture.debugElement.query(
        By.css('#user-exists-validation-message')
      );
      expect(validationElement).toBeTruthy();
    })
  );

  it(
    SignUpComponent.prototype.signUp.name + ' when does not exist call signUp',
    fakeAsync(() => {
      fixture.detectChanges();
      component.userNameControl.setValue('abc');
      fixture.detectChanges();
      const submitElement = fixture.debugElement.query(
        By.css('#submit')
      ).nativeElement;
      signUpServiceSpy.userExists.and.returnValue(asyncData(false));
      submitElement.click();
      tick();
      fixture.detectChanges();
      expect(signUpServiceSpy.signUp.calls.mostRecent().args[0]).toBe('abc');
    })
  );

  it('should change `userAlreadyExists` to false when input changes', () => {
    fixture.detectChanges();
    component.userAlreadyExists = true;
    component.userNameControl.setValue('abc');
    expect(component.userAlreadyExists).toBeFalse();
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
    component.userAlreadyExists = true;
    component.userNameControl.setValue('abc');
    fixture.detectChanges();
    expect(nativeElement.disabled).toBeFalse();
  });
});
