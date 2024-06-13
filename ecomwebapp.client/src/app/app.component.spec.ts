import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { IOrder } from './Models/order'; // Assuming your IOrder interface is in this path

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule, HttpClientModule]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve orders from the server', () => {
    const mockOrders: IOrder[] = [
      { id: 1, name: 'John Doe', address: '123 Main St', email: 'john@example.com', phoneNumber: 1234567890, service: [1, 2] },
      { id: 2, name: 'Jane Smith', address: '456 Elm St', email: 'jane@example.com', phoneNumber: 9876543210, service: [2, 3] },
      // Add more mock order objects as needed
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('https://localhost:7248/api/Orders');
    expect(req.request.method).toEqual('GET');

    req.flush(mockOrders);

    expect(component.orders).toEqual(mockOrders);
  });
});
