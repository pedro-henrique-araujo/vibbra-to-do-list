import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginService } from './login.service';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  private loginService = inject(LoginService);
  private formBuilder = inject(FormBuilder);
  public formGroup = this.formBuilder.group({
    userName: this.formBuilder.control('', Validators.required),
  });

  public userNameControl = this.formGroup.controls.userName;

  public userIsInvalid = false;

  public ngOnInit() {
    this.formGroup.valueChanges.subscribe(() => (this.userIsInvalid = false));
  }

  public login(event: Event) {
    event.preventDefault();
    if (!this.userNameControl.value) return;
    this.loginService.login(this.userNameControl.value).subscribe((invalid) => {
      this.userIsInvalid = invalid;
    });
  }
}
