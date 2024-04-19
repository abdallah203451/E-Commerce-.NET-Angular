import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, switchMap, throwError } from 'rxjs';
import { TokenApiModel } from '../models/JwtAuth';
import { AuthService } from './auth.service';
import { inject } from '@angular/core';
import { Router } from "@angular/router";
import { NgToastService } from 'ng-angular-popup';

export const authInterceptorInterceptor: HttpInterceptorFn = (request, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const toast = inject(NgToastService);
  request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
  let token: string | null = localStorage.getItem("accessToken");
  if (token) {
      request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
  }
  return next(request).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse) {
        // Handle HTTP errors
        if (err.status === 401) {
          // Specific handling for unauthorized errors         
          let tokeApiModel = new TokenApiModel();
          tokeApiModel.token = localStorage.getItem("accessToken") as string;
          tokeApiModel.refreshToken = localStorage.getItem("refreshToken") as string;

          const obj ={
            userId: localStorage.getItem("userId"),
            token: tokeApiModel.token,
            refreshToken: tokeApiModel.refreshToken,
          }

          return auth.renewToken(obj)
          .pipe(
            switchMap((data:TokenApiModel)=>{
              localStorage.setItem('accessToken', data.token)
              localStorage.setItem('refreshToken', data.refreshToken)
              request = request.clone({
                setHeaders: {Authorization:`Bearer ${data.token}`}  // "Bearer "+myToken
              })
              return next(request);
            }),
            catchError((err)=>{
              
                toast.warning({detail:"Warning", summary:"Token is expired, Please Login again"});
                router.navigate(['sign-in'])
                return throwError(()=>err)
            })
          )
        } 
      }
      return throwError(() => err); 
    })
  );;
  
};
