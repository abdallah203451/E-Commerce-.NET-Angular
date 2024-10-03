import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthModalService } from '../../services/auth-modal/auth-modal.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-account-security',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './my-account-security.component.html',
  styleUrl: './my-account-security.component.css',
})
export class MyAccountSecurityComponent {
  passwordForm!: FormGroup;
  PasswordDto = {
    oldPassword: '',
    newPassword: '',
    newPassword1: '',
  };

  constructor(
    private authService: AuthService,
    private authModalService: AuthModalService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.passwordForm = new FormGroup({
      oldPassword: new FormControl(this.PasswordDto.oldPassword, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
      newPassword: new FormControl(this.PasswordDto.newPassword, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
      newPassword1: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
    });
  }

  get f() {
    return this.passwordForm.controls;
  }

  updateProfileData() {
    if (this.PasswordDto.newPassword === this.PasswordDto.newPassword1) {
      const passwordDto = {
        OldPassword: this.PasswordDto.oldPassword,
        NewPassword: this.PasswordDto.newPassword,
      };
      this.authService.changePassword(passwordDto).subscribe({
        next: (data) => {
          console.log('Password changed successfully.');
          localStorage.removeItem('accessToken');
          this.authModalService.logout();
          this.toastr.success(
            'Password changed successfully!',
            'Password changed'
          );
          this.router.navigate(['/sign-in']);
        },
        error: (error) => {
          this.toastr.error(
            'Error while changing password!',
            'changing password failed'
          );
          console.error(error);
        },
      });
    } else {
      this.toastr.error(
        'Password and cinfirm password must be the same!',
        'changing password failed'
      );
    }
  }
}
