import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public register(user: User): Observable<any> {
    return this.http.post<any>(
      'http://localhost:5158/api/Auth/User/Register',
      user
    );
  }

  public login(user: User): Observable<string> {
    return this.http.post('http://localhost:5158/api/Auth/User/Login', user, {
      responseType: 'text',
    });
  }
}
