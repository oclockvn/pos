import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PosPaymentComponent } from './pos-payment.component';

describe('PosPaymentComponent', () => {
  let component: PosPaymentComponent;
  let fixture: ComponentFixture<PosPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PosPaymentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PosPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
