import { CredentialsService } from '@app/auth';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { environment } from '@env/environment';
import { Logger, UntilDestroy, untilDestroyed } from '@core';
import { AuthenticationService } from './authentication.service';
import { SwPush } from '@angular/service-worker';
import { MessagingService } from '@app/@shared/services/messaging.service';

const log = new Logger('Login');

@UntilDestroy()
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  version: string | null = environment.version;
  error: string | undefined;
  loginForm!: FormGroup;
  isLoading = false;

  readonly VAPID_PUBLIC_KEY = environment.vapidPublicKey;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService
  ) {
    this.createForm();
  }

  ngOnInit() {}

  login() {
    this.isLoading = true;
    this.authenticationService
      .login(this.loginForm.value)
      .then((res) => {
        this.isLoading = false;
        if (!res.error) {
          this.notifyNewLogin();
          this.error = undefined;
          const data = {
            username: res.data.name,
            token: res.data.accessToken,
          };
          this.loginForm.markAsPristine();
          untilDestroyed(this);
          this.router.navigate([this.route.snapshot.queryParams.redirect || '/'], { replaceUrl: true });
          this.authenticationService.credentialsService.setCredentials(data, true);
        } else {
          console.log(res.message);
          this.error = res.message;
        }
      })
      .finally(() => (this.isLoading = false));
  }
  notifyNewLogin() {}

  private createForm() {
    this.loginForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
}
