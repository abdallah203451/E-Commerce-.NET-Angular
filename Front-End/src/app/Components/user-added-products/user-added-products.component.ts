import { Component } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { error } from 'console';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-added-products',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './user-added-products.component.html',
  styleUrl: './user-added-products.component.css',
})
export class UserAddedProductsComponent {
  products: any[] = [];

  constructor(
    private productService: ProductService,
    public _sanitizer: DomSanitizer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(): void {
    this.productService.getUserAddedProducts().subscribe({
      next: (data) => {
        this.products = data as any[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe({
      next: (res) => {
        console.log(res);
        for (let p of this.products) {
          if (p.product.id === id) {
            this.products.splice(this.products.indexOf(p), 1);
          }
        }
        this.toastr.success(
          'Product deletted successfully!',
          'Product deletted'
        );
      },
      error: (err) => {
        this.toastr.error(
          'Error while deleting product!',
          'deleting product failed'
        );
        console.log(err);
      },
    });
  }
}
