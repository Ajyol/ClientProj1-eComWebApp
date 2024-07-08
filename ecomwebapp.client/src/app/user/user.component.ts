import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop/shop.service';
import { IOrder } from '../shared/Models/order';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  orders: IOrder[] = [];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.fetchOrders();
  }

  fetchOrders(): void {
    this.shopService.getOrders().subscribe({
      next: (data) => this.orders = data,
      error: (err) => console.error('Error fetching orders', err)
    });
  }
}
