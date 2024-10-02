import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthModalService {
  private loggedInUserSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  public loggedInUser$: Observable<boolean> =
    this.loggedInUserSubject.asObservable();

  private isAdminSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  public isAdmin$: Observable<boolean> = this.isAdminSubject.asObservable();

  constructor() {}

  login(): void {
    this.loggedInUserSubject.next(true);
  }

  logout(): void {
    this.loggedInUserSubject.next(false);
  }

  admin(): void {
    this.isAdminSubject.next(true);
  }

  user(): void {
    this.isAdminSubject.next(false);
  }
}
