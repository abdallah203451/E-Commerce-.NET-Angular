import { Component } from '@angular/core';
import { MyAccountNavComponent } from '../../Components/my-account-nav/my-account-nav.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-my-account',
  standalone: true,
  imports: [RouterOutlet, MyAccountNavComponent],
  templateUrl: './my-account.component.html',
  styleUrl: './my-account.component.css',
})
export class MyAccountComponent {}
