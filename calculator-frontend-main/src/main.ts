import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import {AppComponent} from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient()]  // ✅ Use provideHttpClient() instead of HttpClientModule
}).catch(err => console.error(err));
