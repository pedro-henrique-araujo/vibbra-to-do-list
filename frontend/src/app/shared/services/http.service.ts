import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class HttpService {
  private httpClient = inject(HttpClient);
  private authService = inject(AuthService);

  public get<T>(path: string, httpParams: HttpParams = new HttpParams()) {
    const options = {
      headers: {},
      params: httpParams,
    };

    if (this.authService.isAuthenticated()) {
      options.headers = this.getAuthenticationHeader();
    }
    return this.httpClient.get<T>(environment.backendBaseUrl + path, options);
  }

  public post<T>(path: string, body: any) {
    const options = {
      headers: {},
    };

    if (this.authService.isAuthenticated()) {
      options.headers = this.getAuthenticationHeader();
    }
    return this.httpClient.post<T>(
      environment.backendBaseUrl + path,
      body,
      options
    );
  }

  public put<T>(path: string, body: any) {
    const options = {
      headers: {},
    };

    if (this.authService.isAuthenticated()) {
      options.headers = this.getAuthenticationHeader();
    }
    return this.httpClient.put<T>(
      environment.backendBaseUrl + path,
      body,
      options
    );
  }

  public delete(path: string) {
    const options = {
      headers: {},
    };
    if (this.authService.isAuthenticated()) {
      options.headers = this.getAuthenticationHeader();
    }

    return this.httpClient.delete(environment.backendBaseUrl + path, options);
  }

  private getAuthenticationHeader() {
    return {
      userId: this.authService.getUserId(),
    };
  }
}
