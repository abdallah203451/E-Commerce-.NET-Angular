import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { CartService } from '../../services/cart.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css',
})
export class PaymentComponent {
  carts: any[] = [];
  totalPrice: number = 0;
  discount: number = 0;
  orderId: number = 0;
  isSuccess: Boolean = false;
  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private toastr: ToastrService
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
    const paymentId = this.route.snapshot.queryParamMap.get('token');
    const payerId = this.route.snapshot.queryParamMap.get('PayerID');
    if (paymentId && payerId) {
      this.orderService
        .pay(paymentId, payerId, Number(localStorage.getItem('orderId')))
        .subscribe({
          next: (res) => {
            console.log(res);
            this.isSuccess = true;
            this.toastr.success(
              'Your transaction has been processed successfully!',
              'Payment Successful!'
            );
            this.cartService.ClearCart().subscribe({
              next: (res) => {
                console.log('cart Cleared', res);
              },
              error: (err) => {
                console.error(err);
              },
            });
          },
          error: (err) => {
            this.toastr.error(
              'there was an error processing your payment!',
              'Payment Failed!'
            );
            console.error(err);
          },
        });
    }
  }
  careateOrder() {
    this.orderService.createOrder(this.carts).subscribe({
      next: (res) => {
        console.log('order created');
        const paymentUrl = res.paymentUrl;
        localStorage.setItem('orderId', res.orderId);
        window.location.href = paymentUrl;
      },
      error: (err) => {
        console.log('Order creation failed', err);
      },
    });
  }
}
