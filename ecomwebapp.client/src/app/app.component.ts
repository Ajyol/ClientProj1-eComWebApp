import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IOrder } from './Models/order';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Corrected typo from "styleUrl" to "styleUrls"
})
export class AppComponent implements OnInit {

  orders: IOrder[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<any>('https://localhost:7248/api/Orders').subscribe(
      (result: IOrder[]) => { // Corrected the type of "result" to match the expected type of orders
        console.log(result);
        this.orders = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'ecomwebapp.client';
}
