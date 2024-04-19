import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { lastValueFrom } from 'rxjs';

import { NotificationService } from '../notification.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuardService implements CanActivate {

  public jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private router: Router, private http: HttpClient, private notification: NotificationService) {}

  async canActivate() {
    const token = localStorage.getItem("accessToken");

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }

    const isRefreshSuccess = await this.refreshingTokens(token);
    if (!isRefreshSuccess) {
      this.router.navigate(['/sign-in']);
    }

    return isRefreshSuccess;
  }

  private async refreshingTokens(token: string | null): Promise<boolean> {
    const refreshToken: string | null = localStorage.getItem("refreshToken");

    if (!token || !refreshToken) {
      return false;
    }

    const tokenModel = JSON.stringify({ Token: token, RefreshToken: refreshToken });

    let isRefreshSuccess: boolean;

    try {
      const response = await lastValueFrom(this.http.post("https://localhost:7238/api/" + "User/refreshtoken", tokenModel));
      const newToken = (<any>response).token;
      const newRefreshToken = (<any>response).refreshToken;
      localStorage.setItem("accessToken", newToken);
      localStorage.setItem("refreshToken", newRefreshToken);
      this.notification.showSuccess("Token renewed successfully", "Success")
      isRefreshSuccess = true;
    }
    catch (ex) {
      this.router.navigate(['/sign-in']);
      isRefreshSuccess = false;
    }
    return isRefreshSuccess;
  }

}
