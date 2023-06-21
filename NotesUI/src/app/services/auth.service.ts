import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs/internal/Observable';
import { Admin } from '../models/admin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public registerUser(user: User): Observable<any> {
    return this.http.post<any>(
      'http://localhost:5158/api/Auth/User/Register',
      user
    );
  }

  public registerAdmin(user: Admin): Observable<any> {
    return this.http.post<any>(
      'http://localhost:5158/api/Auth/Admin/Register',
      user
    );
  }

  public loginUser(user: User): Observable<string> {
    return this.http.post('http://localhost:5158/api/Auth/User/Login', user, {
      responseType: 'text',
    });
  }

  public loginAdmin(user: Admin): Observable<string> {
    return this.http.post('http://localhost:5158/api/Auth/Admin/Login', user, {
      responseType: 'text',
    });
  }
}
