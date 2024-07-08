import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private baseUrl = 'https://localhost:7248/api';

  constructor(private http: HttpClient) {}

  login(userName: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Users/login`, { userName, password });
  }
}
