import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountNavComponent } from './my-account-nav.component';

describe('MyAccountNavComponent', () => {
  let component: MyAccountNavComponent;
  let fixture: ComponentFixture<MyAccountNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyAccountNavComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyAccountNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
