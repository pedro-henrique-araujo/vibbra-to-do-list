import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginService } from './login.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  private loginService = inject(LoginService);
  private activatedRoute = inject(ActivatedRoute);
  private formBuilder = inject(FormBuilder);
  public formGroup = this.formBuilder.group({
    userName: this.formBuilder.control('', Validators.required),
  });

  public userNameControl = this.formGroup.controls.userName;

  public userIsInvalid = false;
  public redirectTo!: string;

  public ngOnInit() {
    this.formGroup.valueChanges.subscribe(() => (this.userIsInvalid = false));

    this.redirectTo = this.activatedRoute.snapshot.queryParams['redirectTo'];
    console.log(this.redirectTo);
  }

  public login(event: Event) {
    event.preventDefault();
    if (!this.userNameControl.value) return;
    this.loginService.login(this.userNameControl.value).subscribe((invalid) => {
      this.userIsInvalid = invalid;
    });
  }
}
