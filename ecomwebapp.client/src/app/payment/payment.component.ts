import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  private stripePromise = loadStripe('pk_test_51PCOszIlp3RX116JX7Kv1GXsltPOgDL4cQp2gTbIHhs7mscAxSNhSAsqZSjSOk4AsSlH0lF6gydQk8YPcEGHu3q100axO2e1cL'); // Use publishable key for frontend
  sessionId: string | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.createCheckoutSession();
  }

  async createCheckoutSession(): Promise<void> {
    try {
      const paymentResponse = await this.http.post<{ sessionId?: string }>('/api/payments/create-checkout-session', {
        services: JSON.parse(localStorage.getItem('selectedServices') || '[]')
      }).toPromise();

      if (paymentResponse && paymentResponse.sessionId) {
        this.sessionId = paymentResponse.sessionId;
        await this.redirectToCheckout();
      } else {
        console.error('Invalid payment response', paymentResponse);
      }
    } catch (error) {
      console.error('Error creating checkout session', error);
    }
  }

  async redirectToCheckout(): Promise<void> {
    const stripe = await this.stripePromise;

    if (stripe && this.sessionId) {
      const { error } = await stripe.redirectToCheckout({
        sessionId: this.sessionId,
      });

      if (error) {
        console.error("Stripe Checkout error", error);
      }
    } else {
      console.error("Stripe not loaded or session ID not set");
    }
  }
}
