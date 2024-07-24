import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private baseUrl = 'https://localhost:7248/api';

  constructor(private http: HttpClient) {}

  login(userName: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/users/login`, { userName, password })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error.error.message);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
