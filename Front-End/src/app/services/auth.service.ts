import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TokenApiModel } from '../models/JwtAuth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private BaseUrl:string = "https://localhost:7238/api/User/"
  constructor (private http: HttpClient) { }
  signUp (userObj: any){
    return this.http.post<any> (`${this.BaseUrl}register`, userObj);
  }
  login (loginObj: any){
    return this.http.post<any>(`${this.BaseUrl}login`, loginObj);
  }
  renewToken(tokenApi : any){
    return this.http.post<any>(`${this.BaseUrl}refreshtoken`, tokenApi)
  }
}
