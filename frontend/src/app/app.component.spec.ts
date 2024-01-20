import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AuthService } from './shared/services/auth.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    const authServiceObjSpy = jasmine.createSpyObj(['AuthService']);
    await TestBed.configureTestingModule({
      providers: [
        AppComponent,
        { provide: AuthService, useValue: authServiceObjSpy },
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
