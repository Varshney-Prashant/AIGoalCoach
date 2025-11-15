import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserLoginRequest } from '../models/user-login-request';
import { UserLoginResponse } from '../models/user-login-reponse';
import { UserRegisterRequest } from '../models/user-register-request';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  private baseUrl= "https://localhost:7233/api/user"

  login(userLoginRequest: UserLoginRequest): Observable<UserLoginResponse> {
    return this.http.post<UserLoginResponse>(`${this.baseUrl}/login`, userLoginRequest);
  }

  register(userRegisterRequest: UserRegisterRequest): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/register`, userRegisterRequest);
  }
}
