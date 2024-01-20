import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpService } from './http.service';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../environments/environment';
import { asyncData } from '../utils/async-data';
import { AuthService } from './auth.service';

describe(HttpService.name, () => {
  let httpService: HttpService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  beforeEach(async () => {
    const httpClientSpyObj = jasmine.createSpyObj('HttpClient', [
      'get',
      'post',
    ]);
    const authServiceSpyObj = jasmine.createSpyObj('AuthService', [
      'isAuthenticated',
      'getUserId',
    ]);
    await TestBed.configureTestingModule({
      providers: [
        HttpService,
        { provide: HttpClient, useValue: httpClientSpyObj },
        { provide: AuthService, useValue: authServiceSpyObj },
      ],
    }).compileComponents();

    httpService = TestBed.inject(HttpService);
    httpClientSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
  });

  it(
    HttpService.prototype.get.name + ' should call get and return observable',
    (done) => {
      const expectedData = {
        id: 1,
        text: 'abc',
      };

      httpClientSpy.get
        .withArgs(environment.backendBaseUrl + '/path', {
          headers: {},
          params: new HttpParams(),
        })
        .and.returnValues(asyncData(expectedData));

      httpService.get<any>('/path').subscribe({
        next(data) {
          expect(data).toEqual(expectedData);
          done();
        },
        error: done.fail,
      });
    }
  );

  it(
    HttpService.prototype.post.name + ' should call post and return observable',
    (done) => {
      const expectedData = {
        id: 1,
        text: 'abc',
      };

      const body = {
        text: 'def',
      };

      httpClientSpy.post
        .withArgs(environment.backendBaseUrl + '/path', body, {
          headers: {},
        })
        .and.returnValues(asyncData(expectedData));

      httpService.post<any>('/path', body).subscribe({
        next(data) {
          expect(data).toEqual(expectedData);
          done();
        },
        error: done.fail,
      });
    }
  );
});
