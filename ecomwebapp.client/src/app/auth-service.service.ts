import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7248/api/users';

  constructor(private http: HttpClient) { }

  forgotPassword(email: string): Observable<any> {
    const url = `${this.apiUrl}/forgot-password`;
    const body = { email };

    return this.http.post(url, body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(
      catchError(error => {
        console.error('Error sending reset password email', error);
        return throwError('Error sending reset password email. Please try again later.');
      })
    );
  }

  resetPassword(email: string, token: string, newPassword: string): Observable<any> {
    const url = `${this.apiUrl}/reset-password`;
    const body = { email, token, newPassword };

    return this.http.post(url, body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(
      catchError(error => {
        console.error('Error resetting password', error);
        return throwError('Error resetting password. Please try again later.');
      })
    );
  }
}
