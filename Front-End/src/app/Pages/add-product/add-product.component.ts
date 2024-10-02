import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from '../../Components/nav-bar/nav-bar.component';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule,
  FormControl,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-product',
  standalone: true,
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css',
  imports: [NavBarComponent, FormsModule, ReactiveFormsModule],
})
export class AddProductComponent {
  productForm!: FormGroup;
  imageUrl!: any;
  Product: any = {
    id: 1,
    name: '',
    description: '',
    quantity: '',
    price: '',
    discount: '',
    image: '',
    categoryId: '',
    UserId: '',
  };

  constructor(
    private prod: ProductService,
    private router: Router,
    private route: ActivatedRoute,
    public sanitizer: DomSanitizer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.fetchProductDetails(productId);
    }
    this.productForm = new FormGroup({
      name: new FormControl(this.Product.name, [
        Validators.required,
        Validators.minLength(2),
      ]),
      description: new FormControl(this.Product.description, [
        Validators.required,
        Validators.minLength(8),
      ]),
      quantity: new FormControl(this.Product.quantity, [Validators.required]),
      price: new FormControl(this.Product.price, [Validators.required]),
      discount: new FormControl(this.Product.discount, [Validators.required]),
      image: new FormControl(this.Product.image, [Validators.required]),
      category: new FormControl(this.Product.category, [Validators.required]),
    });
  }

  fetchProductDetails(id: string): void {
    this.prod.getProductDetails(id).subscribe({
      next: (data) => {
        this.Product.id = data.id as any;
        this.Product.name = data.name as any;
        this.Product.description = data.description as any;
        this.Product.quantity = data.quantity as any;
        this.Product.price = data.price as any;
        this.Product.discount = data.discount as any;
        // this.Product.image = data.image as any;
        this.Product.categoryId = data.categoryId as any;
        // this.imageUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
        //   'data:image/png;base64,' + data.image
        // );
      },
      error: (error) => {
        console.error('Error fetching product details:', error);
      },
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const productId = this.route.snapshot.paramMap.get('id');
      if (productId) {
        this.Product.UserId = localStorage.getItem('userId');
        this.Product.id = Number(productId);
        this.prod.editProduct(this.Product).subscribe({
          next: (response) => {
            this.productForm.reset();
            this.toastr.success(
              'Product edited successfully!',
              'Edited successfully'
            );
            this.router.navigate(['/home']);
          },
          error: (err) => {
            this.toastr.error(
              'Error while editing product!',
              'Editing product failed'
            );
            console.error(err);
            //this.loginForm.reset()
          },
        });
      } else {
        this.Product.UserId = localStorage.getItem('userId');
        this.prod.addProduct(this.Product).subscribe({
          next: (response) => {
            this.productForm.reset();
            this.toastr.success(
              'Product added successfully!',
              'Added successfully'
            );
            this.router.navigate(['/home']);
          },
          error: (err) => {
            this.toastr.error(
              'Error while adding product!',
              'Adding product failed'
            );
            console.error(err);
            //this.loginForm.reset()
          },
        });
      }
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = this.handleReaderLoaded.bind(this);
      reader.readAsDataURL(file);
    }
  }

  handleReaderLoaded(e: any) {
    let base64String = e.target.result;
    // Remove the prefix "data:image/*;base64," from the string
    base64String = base64String.split(',')[1];
    console.log(base64String);
    this.Product.image = base64String;
  }
}
