import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ResetPassword } from '../../models/ResetPassword';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-resest-password',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './resest-password.component.html',
  styleUrl: './resest-password.component.css',
})
export class ResestPasswordComponent {
  resetObj: ResetPassword = {
    Email: '',
    Token: '',
    NewPassword: '',
  };
  samePassword: string = '';
  isSent: boolean = false;
  resetForm!: FormGroup;
  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.resetForm = new FormGroup({
      password: new FormControl(this.resetObj.NewPassword, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
      samePassword: new FormControl(this.samePassword, [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/
        ),
      ]),
    });

    this.route.queryParams.subscribe((params) => {
      this.resetObj.Token = params['token'];
      this.resetObj.Email = params['email'];
    });
  }

  get f() {
    return this.resetForm.controls;
  }

  resetPassword() {
    if (this.resetObj.NewPassword == this.samePassword) {
      this.auth.resetPassword(this.resetObj).subscribe({
        next: (res) => {
          this.router.navigate(['/sign-in']);
        },
        error: (err) => {
          console.error(err);
          alert('Error resetting password. Please try again.');
        },
      });
    } else {
      alert('Passwords do not match');
    }
  }
}
