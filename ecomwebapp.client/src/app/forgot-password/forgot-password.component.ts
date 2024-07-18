import { Component } from '@angular/core';
import { AuthService } from '../auth-service.service'; // Adjust path as per your project structure

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  email: string = '';
  message: string = '';
  error: string = '';

  constructor(private authService: AuthService) { }

  onSubmit() {
    if (!this.email) {
      this.error = 'Please enter your email.';
      return;
    }

    this.authService.forgotPassword(this.email).subscribe(
      response => {
        this.message = 'Password reset instructions sent to your email.';
        this.error = '';
      },
      error => {
        console.error('Error sending reset password email', error);
        this.error = 'Error sending reset password email. Please try again later.';
      }
    );
  }
}
