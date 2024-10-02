import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { SignInComponent } from './Pages/sign-in/sign-in.component';
import { SignUpComponent } from './Pages/sign-up/sign-up.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AuthGuardService } from './guard/auth-guard.service';
import { ShopComponent } from './Pages/shop/shop.component';
import { AddProductComponent } from './Pages/add-product/add-product.component';
import { authenticationGuard } from './guard/auth-guard-function';
import { CartComponent } from './Pages/cart/cart.component';
import { PaymentComponent } from './Pages/payment/payment.component';
import { MyAccountComponent } from './Pages/my-account/my-account.component';
import { MyAccountBillingComponent } from './Components/my-account-billing/my-account-billing.component';
import { MyAccountProfileComponent } from './Components/my-account-profile/my-account-profile.component';
import { MyAccountSecurityComponent } from './Components/my-account-security/my-account-security.component';
import { ForgotPasswordComponent } from './Pages/forgot-password/forgot-password.component';
import { ResestPasswordComponent } from './Pages/resest-password/resest-password.component';
import { ProductDetailsComponent } from './Pages/product-details/product-details.component';
import { UserAddedProductsComponent } from './Components/user-added-products/user-added-products.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  {
    path: 'shop/:shoptype',
    component: ShopComponent,
    //canActivate: [authenticationGuard()],
  },
  {
    path: 'details/:id',
    component: ProductDetailsComponent,
    canActivate: [authenticationGuard()],
  },
  {
    path: 'add-product',
    component: AddProductComponent,
    canActivate: [authenticationGuard()],
  },
  {
    path: 'add-product/:id',
    component: AddProductComponent,
    canActivate: [authenticationGuard()],
  },
  {
    path: 'cart',
    component: CartComponent,
    canActivate: [authenticationGuard()],
  },
  {
    path: 'payment',
    component: PaymentComponent,
    canActivate: [authenticationGuard()],
  },
  {
    path: 'my-account',
    component: MyAccountComponent,
    canActivate: [authenticationGuard()],
    children: [
      { path: 'my-account-profile', component: MyAccountProfileComponent },
      { path: 'my-account-billing', component: MyAccountBillingComponent },
      { path: 'my-account-security', component: MyAccountSecurityComponent },
      { path: 'user-added-products', component: UserAddedProductsComponent },
      { path: '', redirectTo: 'my-account-profile', pathMatch: 'full' },
    ],
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent,
  },
  {
    path: 'reset-password',
    component: ResestPasswordComponent,
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
];
