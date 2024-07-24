import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserCreateDto, UserGetDto } from '../shared/Models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserCreateService {
  baseUrl = 'https://localhost:7248/api/users';
  
  constructor(private http: HttpClient) { }

  createUser(user: UserCreateDto) : Observable<UserGetDto>{
    return this.http.post<UserGetDto>(`${this.baseUrl}`, user);
  }
}
