import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SignUpService } from './sign-up.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './sign-up.component.html',
})
export class SignUpComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  private signUpService = inject(SignUpService);
  private formBuilder = inject(FormBuilder);
  public formGroup = this.formBuilder.group({
    userName: this.formBuilder.control('', Validators.required),
  });

  public userAlreadyExists = false;

  public userNameControl = this.formGroup.controls.userName;
  public redirectTo!: string;

  public ngOnInit() {
    this.formGroup.valueChanges.subscribe(
      () => (this.userAlreadyExists = false)
    );
    this.redirectTo = this.activatedRoute.snapshot.queryParams['redirectTo'];
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
