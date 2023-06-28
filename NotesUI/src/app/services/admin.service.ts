import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ServiceResponse } from '../models/serviceResponse';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = 'http://localhost:5158/api/Admin';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<ServiceResponse<User[]>> {
    return this.http.get<ServiceResponse<User[]>>(`${this.apiUrl}/GetAllUsers`);
  }
}
