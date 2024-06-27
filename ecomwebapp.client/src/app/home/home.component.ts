import { Component, OnInit, ViewChild } from '@angular/core';
import { CarouselComponent } from 'ngx-bootstrap/carousel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('carousel') carousel!: CarouselComponent;

  carouselImages = [
    { id: 1, url: 'assets/images/truck1.png' },
    { id: 2, url: 'assets/images/truck2.png' },
    { id: 3, url: 'assets/images/truck3.png' }
  ];

  constructor() {}

  ngOnInit() {}

  // Function to handle swipe events
  handleSwipe(event: any) {
    if (event.deltaX > 0) {
      this.carousel.nextSlide(); // Go to the next slide
    }
  }
}
