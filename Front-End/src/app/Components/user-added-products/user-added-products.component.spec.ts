import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAddedProductsComponent } from './user-added-products.component';

describe('UserAddedProductsComponent', () => {
  let component: UserAddedProductsComponent;
  let fixture: ComponentFixture<UserAddedProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserAddedProductsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserAddedProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
