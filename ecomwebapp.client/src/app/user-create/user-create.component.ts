import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserCreateService } from './user-create.service';
import { UserCreateDto } from '../shared/Models/user.model';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrl: './user-create.component.css'
})
export class UserCreateComponent {
  userForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserCreateService) {
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
      this.userService.createUser(newUser).subscribe(
        response => {
          console.log('User created successfully', response);
        },
        error => {
          console.error('Error creating user', error);
        }
      );
      
    }
  }

}
