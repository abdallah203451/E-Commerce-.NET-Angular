import { Component } from '@angular/core';
import {
  RouterLink,
  RouterLinkActive,
  RouterModule,
  RouterOutlet,
} from '@angular/router';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { HomeComponent } from './Pages/home/home.component';
import { SignUpComponent } from './Pages/sign-up/sign-up.component';
import { SignInComponent } from './Pages/sign-in/sign-in.component';
// import { NgToastModule } from 'ng-angular-popup';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [
    RouterOutlet,
    NavBarComponent,
    HomeComponent,
    SignUpComponent,
    SignInComponent,
    NavBarComponent,
    // NgToastModule,
  ],
})
export class AppComponent {
  title = 'Front-End';
}
