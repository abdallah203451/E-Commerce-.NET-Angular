import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from "../../Components/nav-bar/nav-bar.component";
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-add-product',
  standalone: true,
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css',
  imports: [NavBarComponent, FormsModule, ReactiveFormsModule]
})
export class AddProductComponent {
  productForm!: FormGroup;

  Product: any = {
    name: '',
    description: '',
    quantity: '',
    price: '',
    discount: '',
    image: '',
    categoryId: '',
    userIdAddedBy: '',
  };

  constructor(private prod: ProductService, private router: Router) { }

  ngOnInit(): void {
    (this.productForm = new FormGroup({
      name: new FormControl(this.Product.name, [
        Validators.required,
        Validators.minLength(2),
      ]),
      description: new FormControl(this.Product.description, [
        Validators.required,
        Validators.minLength(8),
      ]),
      quantity: new FormControl(this.Product.quantity, [
        Validators.required,
      ]),
      price: new FormControl(this.Product.price, [
        Validators.required,
      ]),
      discount: new FormControl(this.Product.discount, [
        Validators.required,
      ]),
      image: new FormControl(this.Product.image, [
        Validators.required,
      ]),
      category: new FormControl(this.Product.category, [
        Validators.required,
      ]),
    }));
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.Product.userIdAddedBy = localStorage.getItem("userId");
      this.prod.addProduct(this.Product).subscribe({
        next: (response) => {
          this.productForm.reset();
          this.router.navigate(['/home']);
        },
        error: (err) => {
          alert(err?.error?.message);
          console.error(err)
          //this.loginForm.reset()
        },
      });
    }
  }

  onFileSelected(event:any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = this.handleReaderLoaded.bind(this);
      reader.readAsDataURL(file);
    }
  }
  
  handleReaderLoaded(e:any) {
    let base64String = e.target.result;
    // Remove the prefix "data:image/*;base64," from the string
    base64String = base64String.split(',')[1];
    console.log(base64String);
    this.Product.image = base64String;
  }

}
