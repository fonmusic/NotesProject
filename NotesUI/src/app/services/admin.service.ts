import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ServiceResponse } from '../models/serviceResponse';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = `${environment.apiUrl}/Admin`;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<ServiceResponse<User[]>> {
    return this.http.get<ServiceResponse<User[]>>(`${this.apiUrl}/GetAllUsers`);
  }

  deleteUserById(id: number): Observable<ServiceResponse<any>> {
    return this.http.delete<ServiceResponse<any>>(`${this.apiUrl}/DeleteUserById/${id}`);
  }
}
