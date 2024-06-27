import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IOrder } from '../shared/Models/order';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent implements OnInit{
  orders: IOrder[] = [];

  constructor(private shopService : ShopService) {    }

  ngOnInit(): void {
    this.shopService.getOrders().subscribe(response => {
      this.orders = response;
    }, error => {
      console.log(error);
    });
  }

}
