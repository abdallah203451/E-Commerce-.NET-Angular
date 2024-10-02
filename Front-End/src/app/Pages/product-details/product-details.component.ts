import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { error } from 'console';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductDetailsComponent {
  product: any;
  quantity: number = 1;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    public _sanitizer: DomSanitizer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    this.fetchProductDetails(productId!);
  }

  fetchProductDetails(id: string): void {
    this.productService.getProductDetails(id).subscribe({
      next: (data) => {
        this.product = data as any;
      },
      error: (error) => {
        console.error('Error fetching product details:', error);
      },
    });
  }

  addToCart(): void {
    this.cartService
      .AddCartItem(Number(this.route.snapshot.paramMap.get('id')))
      .subscribe({
        next: (data) => {
          console.log('product added successfully to cart');
          this.toastr.success(
            'Product added to cart successfully!',
            'Added successfully'
          );
        },
        error: (error) => {
          console.error('Error adding product to cart:', error);
          this.toastr.error(
            'Error while adding product to cart!',
            'Adding product failed'
          );
        },
      });
  }
}
