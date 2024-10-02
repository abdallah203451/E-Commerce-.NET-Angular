import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private BaseUrl: string = 'https://localhost:7238/api/Cart/';
  constructor(private http: HttpClient) {}
  GetCartItems() {
    return this.http.get<any>(`${this.BaseUrl}GetCartItems`);
  }

  AddCartItem(productId: number) {
    return this.http.post<any>(`${this.BaseUrl}AddCartItem`, productId);
  }

  UpdateCartItem(cartItemId: number, quantity: number) {
    return this.http.put<any>(
      `${this.BaseUrl}UpdateCartItem/${cartItemId}`,
      quantity
    );
  }

  RemoveCartItem(cartItemId: number) {
    return this.http.delete<any>(`${this.BaseUrl}RemoveCartItem/${cartItemId}`);
  }

  ClearCart() {
    return this.http.delete<any>(`${this.BaseUrl}ClearCart`);
  }
}
