import { Component } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-account-billing',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-account-billing.component.html',
  styleUrl: './my-account-billing.component.css',
})
export class MyAccountBillingComponent {
  orders: any[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit() {
    this.orderService.getOrders().subscribe({
      next: (orders) => {
        this.orders = orders as any[];
      },
      error: (error) => {
        console.error(error);
      },
    });
  }
}
