import { Component } from '@angular/core';
import { NavBarComponent } from "../../Components/nav-bar/nav-bar.component";

@Component({
    selector: 'app-cart',
    standalone: true,
    templateUrl: './cart.component.html',
    styleUrl: './cart.component.css',
    imports: [NavBarComponent]
})
export class CartComponent {

}
