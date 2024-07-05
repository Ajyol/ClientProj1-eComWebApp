import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ShopComponent } from '../shop/shop.component';
import { ShopModule } from '../shop/shop.module';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    CarouselModule.forRoot(),  // Import CarouselModule here
    ShopModule
  ],
  exports: [HomeComponent]
})
export class HomeModule { }
