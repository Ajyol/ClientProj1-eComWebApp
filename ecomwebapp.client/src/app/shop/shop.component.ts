import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent {

  constructor(private router: Router) { }

  navigateTo(service: string) {
    this.router.navigate(['/order'], { queryParams: { service } });
  }
}
