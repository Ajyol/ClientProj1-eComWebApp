import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private router: Router, private viewportScroller: ViewportScroller) {}

  scrollToServices() {
    this.router.navigate(['/']).then(() => {
      const element = document.querySelector('.services');
      if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
      }
    });
  }

  ngOnInit() {}
}
