import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-account-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './my-account-profile.component.html',
  styleUrl: './my-account-profile.component.css',
})
export class MyAccountProfileComponent {
  Profiledata = {
    fullName: '',
    email: '',
    phone: '',
  };

  constructor(
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.authService.getProfileData().subscribe({
      next: (data) => {
        this.Profiledata.fullName = data.fullName;
        this.Profiledata.email = data.email;
        this.Profiledata.phone = data.phone;
      },
      error: (error) => {
        console.error(error);
      },
    });
  }

  updateProfileData() {
    this.authService.UpdateProfileData(this.Profiledata).subscribe({
      next: (data) => {
        this.toastr.success(
          'Profile data updated successfully!',
          'Updated successfully'
        );
        console.log('profile updated');
      },
      error: (error) => {
        this.toastr.error(
          'Error while update profile data!',
          'Updating profile failed'
        );
        console.error(error);
      },
    });
  }
}
