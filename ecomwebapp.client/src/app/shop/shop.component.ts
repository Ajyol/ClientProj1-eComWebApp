import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IOrder } from '../shared/Models/order';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  orders: IOrder[] = [];

  constructor(private shopService: ShopService, private router: Router) { }

  ngOnInit(): void {
    this.shopService.getOrders().subscribe(response => {
      this.orders = response;
    }, error => {
      console.log(error);
    });
  }

  navigateTo(service: string) {
    this.router.navigate(['/order'], { queryParams: { service } });
  }
}
