import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { CoreModule } from "../core/core.module";



@NgModule({
    declarations: [
        ShopComponent
    ],
    imports: [
        CommonModule,
        CoreModule
    ],
    exports: [
      ShopComponent
    ]
})
export class ShopModule { }
