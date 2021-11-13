import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + 'Identity/Login';
  private registerPath = environment.apiUrl + 'Identity/Register';

  constructor(private http: HttpClient) {

  }

  login(data): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  register(data): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  setToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    localStorage.getItem('token');
  }
}
