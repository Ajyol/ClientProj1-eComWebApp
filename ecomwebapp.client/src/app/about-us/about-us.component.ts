import { Component } from '@angular/core';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {
  developers = [
    { name: 'Ajyol Dhamala', imageSrc: 'assets/images/ajyol.heic', altText: 'Ajyol Dhamala' },
  ] 
}
