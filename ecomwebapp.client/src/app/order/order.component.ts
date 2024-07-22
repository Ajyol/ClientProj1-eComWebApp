import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from '../shared/Models/order';
import { loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orderForm: FormGroup;
  selectedService: string | null = null;
  private stripePromise = loadStripe('pk_test_51PCOszIlp3RX116JX7Kv1GXsltPOgDL4cQp2gTbIHhs7mscAxSNhSAsqZSjSOk4AsSlH0lF6gydQk8YPcEGHu3q100axO2e1cL'); // Use publishable key for frontend
  isLoading = false; // Add loading state

  services = [
    { id: 1, name: 'Service 1', price: 10 },
    { id: 2, name: 'Service 2', price: 20 },
    { id: 3, name: 'Service 3', price: 30 },
  ];

  constructor(private fb: FormBuilder, private http: HttpClient, private route: ActivatedRoute) {
    this.orderForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      service: this.fb.array(this.services.map(() => this.fb.control(false)))
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.selectedService = params['service'] || null;
      this.setSelectedService();
    });
  }

  setSelectedService(): void {
    if (this.selectedService) {
      const serviceIndex = this.services.findIndex(service => service.name === this.selectedService);
      if (serviceIndex !== -1) {
        (this.orderForm.get('service') as FormArray).controls[serviceIndex].setValue(true);
      }
    }
  }

  get isServiceSelected(): boolean {
    return (this.orderForm.get('service') as FormArray).controls.some(control => (control as FormControl).value);
  }

  async onSubmit(): Promise<void> {
    if (this.orderForm.valid && this.isServiceSelected) {
      this.isLoading = true; // Start loading indicator

      const selectedServices = (this.orderForm.get('service') as FormArray).controls
        .map((control: AbstractControl, index: number) => (control as FormControl).value ? this.services[index] : null)
        .filter((value: { id: number, name: string, price: number } | null) => value !== null) as { id: number, name: string, price: number }[];
      
      const order: IOrder = {
        ...this.orderForm.value,
        service: selectedServices.map(service => service.id)
      };

      try {
        // Post the order to your backend
        const response = await this.http.post<IOrder>('https://localhost:7248/api/Orders', order).toPromise();
        console.log('Order submitted', response);

        // Get the payment session ID from your backend
        const paymentResponse = await this.http.post<{ sessionId?: string }>('/api/payments/create-checkout-session', {
          services: selectedServices
        }).toPromise();

        if (paymentResponse?.sessionId) {
          const stripe = await this.stripePromise;

          if (stripe) {
            const { error } = await stripe.redirectToCheckout({
              sessionId: paymentResponse.sessionId,
            });

            if (error) {
              console.error("Stripe Checkout error", error);
            }
          } else {
            console.error("Stripe not loaded");
          }
        } else {
          console.error('Invalid payment response', paymentResponse);
        }
      } catch (error) {
        console.error('Error submitting order', error);
      } finally {
        this.isLoading = false; // Stop loading indicator
      }
    } else {
      console.log('Form is invalid or no service selected');
    }
  }
}
