import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private BaseUrl: string = 'https://localhost:7238/api/Order/';
  //private BaseUrl: string = 'https://quickbuy.runasp.net/api/Order/';
  constructor(private http: HttpClient) {}
  createOrder(obj: any) {
    return this.http.post<any>(`${this.BaseUrl}create`, obj);
  }

  pay(paymentId: string, payerId: string, orderId: number) {
    const params = new HttpParams()
      .set('paymentId', paymentId)
      .set('payerId', payerId)
      .set('orderId', orderId);
    return this.http.get<any>(`${this.BaseUrl}payment`, { params });
  }

  getOrders() {
    return this.http.get<any>(`${this.BaseUrl}get-orders`);
  }
}
