import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountProfileComponent } from './my-account-profile.component';

describe('MyAccountProfileComponent', () => {
  let component: MyAccountProfileComponent;
  let fixture: ComponentFixture<MyAccountProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyAccountProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyAccountProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
