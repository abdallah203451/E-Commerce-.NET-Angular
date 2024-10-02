import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
})
export class ForgotPasswordComponent {
  isSent: boolean = false;
  forgotForm!: FormGroup;

  constructor(private auth: AuthService) {}

  ngOnInit(): void {
    this.forgotForm = new FormGroup({
      emailForm: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  get f() {
    return this.forgotForm.controls;
  }

  forgotPassword() {
    if (this.forgotForm.valid) {
      const email = this.forgotForm.value.emailForm;
      this.auth.forgotPassword(email).subscribe({
        next: (res) => {
          this.isSent = true;
        },
        error: (err) => {
          console.error(err);
          alert('error while sending email');
        },
      });
    }
  }
}
