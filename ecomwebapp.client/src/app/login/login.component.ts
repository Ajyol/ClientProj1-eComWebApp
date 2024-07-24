// login.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private loginService: LoginService, private router: Router) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { userName, password } = this.loginForm.value;
      this.loginService.login(userName, password).subscribe({
        next: (response) => {
          console.log('Login successful', response);
          this.router.navigate(['/user']);
        },
        error: (error) => {
          console.error('Login error', error);
          this.errorMessage = error.message || 'Invalid username or password';
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
