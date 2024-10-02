import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthModalService } from '../../services/auth-modal/auth-modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-account-nav',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './my-account-nav.component.html',
  styleUrl: './my-account-nav.component.css',
})
export class MyAccountNavComponent {
  isAdmin: boolean = false;

  ngOnInit() {
    if (localStorage.getItem('role') == 'Administrator') {
      this.isAdmin = true;
    }
  }
}
