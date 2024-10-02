import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private BaseUrl: string = 'https://localhost:7238/api/Product/';
  constructor(private http: HttpClient) {}
  addProduct(product: any) {
    return this.http.post<any>(`${this.BaseUrl}addproduct`, product);
  }

  getAllProducts(filterDto: any) {
    //let queryParams = new HttpParams();
    //queryParams = queryParams.append('filterDto', filterDto);
    return this.http.get<any>(`${this.BaseUrl}getallproducts`, {
      params: filterDto,
    });
  }

  getTrendingProducts(filterDto: any) {
    //let queryParams = new HttpParams();
    //queryParams = queryParams.append('filterDto', filterDto);
    return this.http.get<any>(`${this.BaseUrl}gettrendingproducts`, {
      params: filterDto,
    });
  }

  getProductDetails(id: any) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    return this.http.get<any>(`${this.BaseUrl}getproductdetails`, {
      params: queryParams,
    });
  }

  getUserAddedProducts() {
    return this.http.get<any>(`${this.BaseUrl}user-products`);
  }

  deleteProduct(id: number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('id', id);
    return this.http.delete<any>(`${this.BaseUrl}delete-product`, {
      params: queryParams,
    });
  }

  editProduct(product: any) {
    return this.http.put<any>(`${this.BaseUrl}edit-product`, product);
  }
}
