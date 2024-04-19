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

export const routes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'sign-in', component: SignInComponent},
    {path: 'sign-up', component: SignUpComponent },//, canActivate: [AuthGuardService]
    {path: 'shop', component: ShopComponent, canActivate: [authenticationGuard()]},
    {path: 'add-product', component: AddProductComponent, canActivate: [authenticationGuard()]},
    {path: 'cart', component: CartComponent, canActivate: [authenticationGuard()]},
    {path: '', redirectTo: 'home', pathMatch: 'full'},
];
