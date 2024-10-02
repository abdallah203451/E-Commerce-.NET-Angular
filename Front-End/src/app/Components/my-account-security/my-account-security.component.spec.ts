import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountSecurityComponent } from './my-account-security.component';

describe('MyAccountSecurityComponent', () => {
  let component: MyAccountSecurityComponent;
  let fixture: ComponentFixture<MyAccountSecurityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyAccountSecurityComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyAccountSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
