import { Component } from '@angular/core';
import { NavBarComponent } from "../../Components/nav-bar/nav-bar.component";
import { FooterComponent } from "../../Components/footer/footer.component";
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [NavBarComponent, FooterComponent, RouterLink, RouterLinkActive,]
})
export class HomeComponent {
    categories(id: number) {
        localStorage.setItem("category", id.toString());
    }
}
