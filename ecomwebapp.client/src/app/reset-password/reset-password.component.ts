import { AuthService } from '../auth-service.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  token: string = '';
  email: string = '';
  newPassword: string = '';
  message: string = '';
  error: string = '';

  constructor(private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.token = this.route.snapshot.paramMap.get('token') || '';
  }

  onSubmit() {
    if (!this.email || !this.newPassword) {
      this.error = 'Please enter all fields.';
      return;
    }

    this.authService.resetPassword(this.email, this.token, this.newPassword).subscribe(
      response => {
        this.message = 'Password reset successfully.';
        this.error = '';
      },
      error => {
        console.error('Error resetting password', error);
        this.error = 'Error resetting password. Please try again later.';
      }
    );
  }
}
