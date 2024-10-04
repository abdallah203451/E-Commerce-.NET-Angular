import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { lastValueFrom } from 'rxjs';

export function authenticationGuard(): CanActivateFn {
  return async () => {
    const router: Router = inject(Router);
    //const jwtHelper: JwtHelperService = inject(JwtHelperService);

    const token = localStorage.getItem('accessToken');
    if (token) {
      const payloadBase64 = token!.split('.')[1];
      const decodedJson = atob(payloadBase64);
      const payload = JSON.parse(decodedJson);
      const exp = payload.exp;
      const currentTime = Math.floor(Date.now() / 1000);
      if (currentTime < exp) {
        return true;
      }
    } else {
      await router.navigate(['/sign-in']);
      return false;
    }

    const isRefreshSuccess = await refreshingTokens(token);
    if (!isRefreshSuccess) {
      await router.navigate(['/sign-in']);
    }

    return isRefreshSuccess;
  };
}

export async function refreshingTokens(token: string | null) {
  const router = inject(Router);
  const http = inject(HttpClient);

  const refreshToken: string | null = localStorage.getItem('refreshToken');
  const userId: string | null = localStorage.getItem('userId');

  if (!token || !refreshToken) {
    return false;
  }

  const tokenModel = JSON.stringify({
    userId: userId,
    token: token,
    refreshToken: refreshToken,
    role: 'User',
  });

  let isRefreshSuccess: boolean;

  try {
    //const response = await lastValueFrom(http.post("https://localhost:7238/api/" + "User/refreshtoken", tokenModel));
    const response = await lastValueFrom(
      http.post(
        'https://quickbuy.runasp.net/api/' + 'User/refreshtoken',
        tokenModel
      )
    );
    const newToken = (<any>response).token;
    const newRefreshToken = (<any>response).refreshToken;
    const newUserId = (<any>response).userId;
    localStorage.setItem('accessToken', newToken);
    localStorage.setItem('refreshToken', newRefreshToken);
    localStorage.setItem('userId', newUserId);
    isRefreshSuccess = true;
  } catch (ex) {
    router.navigate(['/sign-in']);
    isRefreshSuccess = false;
  }
  return isRefreshSuccess;
}
