// import { Component } from '@angular/core';
// import { NavBarComponent } from '../../Components/nav-bar/nav-bar.component';
// import { ProductService } from '../../services/product.service';
// import { ActivatedRoute, Router, RouterLink } from '@angular/router';
// import { CommonModule, NgFor, NgIf } from '@angular/common';
// import { DomSanitizer } from '@angular/platform-browser';
// import { CartService } from '../../services/cart.service';
// import { FormsModule } from '@angular/forms';

// @Component({
//   selector: 'app-shop',
//   standalone: true,
//   templateUrl: './shop.component.html',
//   styleUrl: './shop.component.css',
//   imports: [
//     NavBarComponent,
//     NgFor,
//     NgIf,
//     CommonModule,
//     RouterLink,
//     FormsModule,
//   ],
// })
// export class ShopComponent {
//   products: any[] = [];
//   numberOfPages: number = 1;
//   shopType: number = 1;
//   filterDto = {
//     Category: 1,
//     MinPrice: 0,
//     MaxPrice: 0,
//     Name: '',
//     PageNumber: 1,
//   };
//   constructor(
//     private prod: ProductService,
//     private cartService: CartService,
//     public _sanitizer: DomSanitizer,
//     private route: ActivatedRoute
//   ) {}

//   ngOnInit() {
//     this.shopType = Number(this.route.snapshot.paramMap.get('shoptype'));

//     if (this.shopType == 1) {
//       var categoryId = Number(localStorage.getItem('category'));
//       this.filterDto.Category = categoryId;
//       this.prod.getAllProducts(this.filterDto).subscribe({
//         next: (res) => {
//           this.products = res.items as any[];
//           this.numberOfPages = res.totalPages as number;
//         },
//         error: (err) => console.error(err),
//       });
//     } else {
//       this.prod.getTrendingProducts(this.filterDto).subscribe({
//         next: (res) => {
//           this.products = res.items as any[];
//           this.numberOfPages = res.totalPages as number;
//         },
//         error: (err) => console.error(err),
//       });
//     }
//   }

//   addToCart(productId: number) {
//     this.cartService.AddCartItem(productId).subscribe({
//       next: (res) => {
//         console.log('product added to cart');
//       },
//       error: (err) => {
//         console.log(err);
//       },
//     });
//   }

//   applyFilters() {
//     this.filterDto.PageNumber = 1; // Reset to first page when filters are applied
//     this.fetchProducts();
//   }

//   changePage(pageNumber: number) {
//     if (pageNumber >= 1 && pageNumber <= this.numberOfPages) {
//       this.filterDto.PageNumber = pageNumber;
//       this.fetchProducts();
//     }
//   }

//   fetchProducts() {
//     this.prod.getAllProducts(this.filterDto).subscribe({
//       next: (res) => {
//         this.products = res.items;
//         this.numberOfPages = res.totalPages;
//       },
//       error: (err) => console.error(err),
//     });
//   }
// }
import { Component } from '@angular/core';
import { NavBarComponent } from '../../Components/nav-bar/nav-bar.component';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { CartService } from '../../services/cart.service';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shop',
  standalone: true,
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
  imports: [
    NavBarComponent,
    NgFor,
    NgIf,
    CommonModule,
    RouterLink,
    FormsModule,
  ],
})
export class ShopComponent {
  products: any[] = [];
  numberOfPages: number = 1;
  shopType: number = 1;
  loading: boolean = true; // Track loading state
  filterDto = {
    Category: 1,
    MinPrice: 0,
    MaxPrice: 0,
    Name: '',
    PageNumber: 1,
  };

  constructor(
    private prod: ProductService,
    private cartService: CartService,
    public _sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.shopType = Number(this.route.snapshot.paramMap.get('shoptype'));

    if (this.shopType == 1) {
      var categoryId = Number(localStorage.getItem('category'));
      this.filterDto.Category = categoryId;
      this.fetchProducts();
    } else {
      this.fetchTrendingProducts();
    }
  }

  fetchProducts() {
    this.loading = true; // Start loading
    this.prod.getAllProducts(this.filterDto).subscribe({
      next: (res) => {
        this.products = res.items as any[];
        this.numberOfPages = res.totalPages as number;
        this.loading = false; // Stop loading when products are fetched
      },
      error: (err) => {
        console.error(err);
        this.loading = false; // Stop loading on error
      },
    });
  }

  fetchTrendingProducts() {
    this.loading = true;
    this.prod.getTrendingProducts(this.filterDto).subscribe({
      next: (res) => {
        this.products = res.items as any[];
        this.numberOfPages = res.totalPages as number;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      },
    });
  }

  addToCart(productId: number) {
    this.cartService.AddCartItem(productId).subscribe({
      next: (res) => {
        console.log('product added to cart');
        this.toastr.success(
          'Product added to cart successfully!',
          'Added successfully'
        );
      },
      error: (err) => {
        console.log(err);
        this.toastr.error(
          'Error while adding product to cart!',
          'Adding product failed'
        );
      },
    });
  }

  applyFilters() {
    this.filterDto.PageNumber = 1;
    this.fetchProducts(); // Fetch products with applied filters
  }

  changePage(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.numberOfPages) {
      this.filterDto.PageNumber = pageNumber;
      this.fetchProducts();
    }
  }
}
