import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountBillingComponent } from './my-account-billing.component';

describe('MyAccountBillingComponent', () => {
  let component: MyAccountBillingComponent;
  let fixture: ComponentFixture<MyAccountBillingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyAccountBillingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyAccountBillingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
