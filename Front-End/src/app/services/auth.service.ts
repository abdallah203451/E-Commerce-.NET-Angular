import { HttpClientModule, HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TokenApiModel } from '../models/JwtAuth';
import { ResetPassword } from '../models/ResetPassword';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private BaseUrl: string = 'https://localhost:7238/api/User/';
  constructor(private http: HttpClient) {}
  signUp(userObj: any) {
    return this.http.post<any>(`${this.BaseUrl}register`, userObj);
  }
  login(loginObj: any) {
    return this.http.post<any>(`${this.BaseUrl}login`, loginObj);
  }
  renewToken(tokenApi: any) {
    return this.http.post<any>(`${this.BaseUrl}refreshtoken`, tokenApi);
  }

  forgotPassword(email: string) {
    return this.http.post<any>(`${this.BaseUrl}forgot-password`, { email });
  }

  resetPassword(resetPassword: ResetPassword) {
    resetPassword.Token = decodeURIComponent(resetPassword.Token);
    return this.http.post<any>(`${this.BaseUrl}reset-password`, resetPassword);
  }

  getProfileData() {
    return this.http.get<any>(`${this.BaseUrl}profile-data`);
  }

  UpdateProfileData(profileDataDto: any) {
    return this.http.put<any>(`${this.BaseUrl}update-profile`, profileDataDto);
  }

  changePassword(passwordDto: any) {
    return this.http.post<any>(`${this.BaseUrl}change-password`, passwordDto);
  }
}
