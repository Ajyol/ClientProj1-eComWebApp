import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IOrder } from '../shared/Models/order';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  orderForm: FormGroup;

  services = [
    { id: 1, name: 'Service 1' },
    { id: 2, name: 'Service 2' },
    { id: 3, name: 'Service 3' },
  ];

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.orderForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      service: this.fb.array(this.services.map(() => this.fb.control(false)))
    });
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const selectedServices = (this.orderForm.get('service') as FormArray).controls
        .map((control: AbstractControl, index: number) => (control as FormControl).value ? this.services[index].id : null)
        .filter((value: number | null) => value !== null) as number[];
      
      const order: IOrder = {
        ...this.orderForm.value,
        service: selectedServices
      };

      this.http.post<IOrder>('https://localhost:7248/api/Orders', order).subscribe({
        next: (response) => console.log('Order submitted', response),
        error: (error) => console.error('Error submitting order', error)
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
