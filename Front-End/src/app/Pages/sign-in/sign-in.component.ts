import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { NavBarComponent } from "../../Components/nav-bar/nav-bar.component";
//import { NotificationService } from '../../notification.service';

@Component({
    selector: 'app-sign-in',
    standalone: true,
    templateUrl: './sign-in.component.html',
    styleUrl: './sign-in.component.css',
    imports: [HttpClientModule, FormsModule, ReactiveFormsModule, NgIf, RouterLink, RouterLinkActive, NavBarComponent]
})
export class SignInComponent {
  public invalidLogin: boolean = false;
  loginForm!: FormGroup;
  phonePattern = '^[0-9]{4}[0-9]{3}[0-9]{4}$';
  public static h= "";

  constructor(private auth: AuthService,  private router: Router) {}

  ngOnInit(): void {
    (this.loginForm = new FormGroup({
      email: new FormControl(this.UserLogin.email, [
        Validators.required,
        Validators.email,
      ]),
      password: new FormControl(this.UserLogin.password, [
        Validators.required,
        Validators.minLength(8),
      ]),
    }));
  }

  // get loginemail() {
  //   return this.loginForm.get('email');
  // }
  // get loginpassword() {
  //   return this.loginForm.get('password');
  // }

  UserLogin: any = {
    email: '',
    password: '',
  };

  onLogin() {
    localStorage.clear();
    if (this.loginForm.valid) {
      const obj = {
        email: this.UserLogin.email,
        password: this.UserLogin.password,
      }
      this.auth.login(this.loginForm.value).subscribe({
        next: (response) => {
          //this.notification.showSuccess("User login successful", "Success")
          const token = (<any>response).token;
          const refreshToken = (<any>response).refreshToken;
          const userId = (<any>response).userId;
          localStorage.setItem("accessToken", token);
          localStorage.setItem("refreshToken", refreshToken);
          localStorage.setItem("userId", userId);
          this.invalidLogin = false;
          this.router.navigate(['/home'])
        },
        error: (err) => {
          alert(err?.error.message);
          //this.notification.showError("Invalid username or password.", "Error")
          console.error(err)
          this.invalidLogin = true;
          //this.loginForm.reset()
        },
      });
    }
  }
}
