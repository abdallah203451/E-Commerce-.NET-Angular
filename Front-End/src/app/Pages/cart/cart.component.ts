import { Component } from '@angular/core';
import { NavBarComponent } from '../../Components/nav-bar/nav-bar.component';
import { CartService } from '../../services/cart.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule, NgFor } from '@angular/common';
import { OrderService } from '../../services/order.service';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
  imports: [NavBarComponent, CommonModule, RouterLink],
})
export class CartComponent {
  carts: any[] = [];
  totalPrice: number = 0;
  discount: number = 0;
  orderId: number = 0;
  constructor(
    private cartService: CartService,
    public _sanitizer: DomSanitizer,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cartService.GetCartItems().subscribe({
      next: (res) => {
        this.carts = res as any[];
        for (let c of this.carts) {
          this.totalPrice += c.price * c.quantity;
          this.discount += (c.discount / 100) * (c.price * c.quantity);
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  updateQuantity(cartItemId: number, quantity: number, index: number) {
    this.carts[index].quantity += quantity;
    this.totalPrice = 0;
    this.discount = 0;
    for (let c of this.carts) {
      this.totalPrice += c.price * c.quantity;
      this.discount += (c.discount / 100) * (c.price * c.quantity);
    }
    this.cartService.UpdateCartItem(cartItemId, quantity).subscribe({
      next: (res) => {
        console.log('cart item updated');
      },
    });
  }

  deleteCart(cartItemId: number, index: number) {
    this.carts.splice(index, 1);
    this.cartService.RemoveCartItem(cartItemId).subscribe({
      next: (res) => {
        console.log('cart item deleted');
      },
      error: (err) => {
        console.log('error deleting cart item');
      },
    });
  }


}
