import { Component } from '@angular/core';
import { NavBarComponent } from "../../Components/nav-bar/nav-bar.component";
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { NgFor, NgIf } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-shop',
    standalone: true,
    templateUrl: './shop.component.html',
    styleUrl: './shop.component.css',
    imports: [NavBarComponent,NgFor,NgIf]
})

export class ShopComponent {
    products:any[]=[];
    constructor(private prod: ProductService, private router: Router, public _sanitizer: DomSanitizer) {
        var categoryId = Number(localStorage.getItem("category"));
        this.prod.getAllProducts(categoryId).subscribe({
            next: (res) => {
                this.products = res as any[];
              }
        });
    }
}
