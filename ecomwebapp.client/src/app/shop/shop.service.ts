import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../shared/Models/order';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7248/api';
  constructor(private http: HttpClient) { }

  getOrders() {
    return this.http.get<IOrder[]>(this.baseUrl + '/Orders');
  }
}
