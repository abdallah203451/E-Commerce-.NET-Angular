import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
// import { Router } from 'express';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterModule,
} from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { NgIf } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
//import { NotificationService } from '../../notification.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [HttpClientModule, FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  public invalidRegister = false;
  registerForm!: FormGroup;
  phonePattern = '^[0-9]{4}[0-9]{3}[0-9]{4}$';
  constructor(
    private auth: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      fullname: new FormControl(this.UserRegister.username, [
        Validators.required,
        Validators.minLength(3),
      ]),
      email: new FormControl(this.UserRegister.email, [
        Validators.required,
        Validators.email,
      ]),
      phone: new FormControl(this.UserRegister.phone, [
        Validators.required,
        Validators.pattern(this.phonePattern),
      ]),
      password: new FormControl(this.UserRegister.password, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
      password1: new FormControl(this.UserRegister.password1, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
      isSeller: new FormControl(this.UserRegister.isSeller, []),
    });
  }
  get fullname() {
    return this.registerForm.get('fullname');
  }
  get email() {
    return this.registerForm.get('email');
  }
  get phone() {
    return this.registerForm.get('phone');
  }
  get password() {
    return this.registerForm.get('password');
  }
  get password1() {
    return this.registerForm.get('password1');
  }

  get f() {
    return this.registerForm.controls;
  }

  UserRegister: any = {
    fullname: '',
    email: '',
    phone: '',
    password: '',
    password1: '',
    isSeller: false,
  };

  onRegister() {
    localStorage.clear();
    if (this.registerForm.valid) {
      // perform logic for signup
      if (this.UserRegister.password == this.UserRegister.password1) {
        this.auth.signUp(this.registerForm.value).subscribe({
          next: (res) => {
            this.invalidRegister = false;
            this.toastr.success(
              'You have successfully sign up!',
              'sign-up Success'
            );
            this.router.navigate(['/sign-in']);
            alert(res.message);
          },
          error: (err) => {
            this.toastr.error('Invalid email', 'Login failed');
          },
        });
      } else {
        this.toastr.error(
          'Password and confirm password must be the same',
          'Login failed'
        );
      }
      // console.log(this.registerForm.value);
    }
  }
}
