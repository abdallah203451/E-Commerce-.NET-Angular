import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private BaseUrl:string = "https://localhost:7238/api/Product/"
  constructor (private http: HttpClient) { }
  addProduct (product: any){
    return this.http.post<any> (`${this.BaseUrl}addproduct`, product);
  }

  getAllProducts (categoryId: number){
    let queryParams = new HttpParams();
    queryParams = queryParams.append("categoryId",categoryId);
    return this.http.get<any> (`${this.BaseUrl}getallproducts`,{params:queryParams});
  }
}
