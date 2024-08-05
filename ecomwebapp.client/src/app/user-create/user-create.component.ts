import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserCreateService } from './user-create.service';
import { UserCreateDto } from '../shared/Models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrl: './user-create.component.css'
})
export class UserCreateComponent {
  userForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserCreateService, private router: Router) {
    this.userForm = this.fb.group({
      userName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      passwordHash: ['', Validators.required],
    });    
  }

  onSubmit(){
    if (this,this.userForm.valid) {
      const newUser : UserCreateDto = this.userForm.value;
      this.userService.createUser(newUser).subscribe({
          next: (response) => {
          console.log('User created successfully', response);
          this.router.navigate(['/login'])
        },
        error: (error) => {
          console.error('Error creating user', error);
        }
      }
      );
      
    }
  }

}
