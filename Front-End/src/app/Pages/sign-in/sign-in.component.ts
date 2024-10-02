import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterModule,
} from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { NavBarComponent } from '../../Components/nav-bar/nav-bar.component';
import { AuthModalService } from '../../services/auth-modal/auth-modal.service';
// import { NgToastModule, NgToastService } from 'ng-angular-popup';
//import { NotificationService } from '../../notification.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-sign-in',
  standalone: true,
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgIf,
    RouterLink,
    RouterLinkActive,
    NavBarComponent,
  ],
})
export class SignInComponent {
  public invalidLogin: boolean = false;
  loginForm!: FormGroup;
  phonePattern = '^[0-9]{4}[0-9]{3}[0-9]{4}$';
  public static h = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private authModalService: AuthModalService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(this.UserLogin.email, [
        Validators.required,
        Validators.email,
      ]),
      password: new FormControl(this.UserLogin.password, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
    });
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

  get f() {
    return this.loginForm.controls;
  }

  onLogin() {
    localStorage.clear();
    if (this.loginForm.valid) {
      const obj = {
        email: this.UserLogin.email,
        password: this.UserLogin.password,
      };
      this.auth.login(this.loginForm.value).subscribe({
        next: (response) => {
          const token = (<any>response).token;
          const refreshToken = (<any>response).refreshToken;
          const userId = (<any>response).userId;
          const role = (<any>response).role;
          localStorage.setItem('accessToken', token);
          localStorage.setItem('refreshToken', refreshToken);
          localStorage.setItem('userId', userId);
          localStorage.setItem('role', role);
          this.invalidLogin = false;
          this.authModalService.login();
          if (role == 'Administrator') {
            this.authModalService.admin();
          } else {
            this.authModalService.user();
          }
          this.toastr.success(
            'You have successfully logged in!',
            'Login Success'
          );
          this.router.navigate(['/home']);
        },
        error: (err) => {
          this.toastr.error('Invalid username or password.', 'Login failed');
          console.error(err);
          this.invalidLogin = true;
          //this.loginForm.reset()
        },
      });
    }
  }
}
