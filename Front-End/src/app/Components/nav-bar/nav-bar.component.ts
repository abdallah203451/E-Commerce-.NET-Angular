import { Component } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { AuthModalService } from '../../services/auth-modal/auth-modal.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',
})
export class NavBarComponent {
  isLogin: boolean = false;
  isAdmin: boolean = false;

  constructor(private authModalService: AuthModalService) {}

  categories(id: number) {
    localStorage.setItem('category', id.toString());
  }

  ngOnInit() {
    this.authModalService.loggedInUser$.subscribe({
      next: (isLogin) => {
        this.isLogin = isLogin;
      },
    });
    this.authModalService.isAdmin$.subscribe({
      next: (isAdmin) => {
        this.isAdmin = isAdmin;
      },
    });

    if (localStorage.getItem('accessToken')) {
      this.isLogin = true;
    }
    if (localStorage.getItem('role') == 'Administrator') {
      this.isAdmin = true;
    }
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.authModalService.logout();
  }
}
