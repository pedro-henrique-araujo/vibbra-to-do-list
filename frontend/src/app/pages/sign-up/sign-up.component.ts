import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SignUpService } from './sign-up.service';

@Component({
  templateUrl: './sign-up.component.html',
})
export class SignUpComponent implements OnInit {
  private signUpService = inject(SignUpService);
  private formBuilder = inject(FormBuilder);
  public formGroup = this.formBuilder.group({
    userName: this.formBuilder.control('', Validators.required),
  });

  public userAlreadyExists = false;

  public userNameControl = this.formGroup.controls.userName;

  public ngOnInit() {
    this.formGroup.valueChanges.subscribe(
      () => (this.userAlreadyExists = false)
    );
  }

  public signUp(event: Event) {
    event.preventDefault();
    const userName = this.userNameControl.value;
    if (!userName) return;
    this.signUpService.userExists(userName).subscribe((exists) => {
      this.userAlreadyExists = exists;
      if (exists) return;
      this.signUpService.signUp(userName);
    });
  }
}
