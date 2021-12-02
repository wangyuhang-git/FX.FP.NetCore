import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginFormRoutingModule } from './login-form-routing.module';
import { LoginFormComponent } from './login-form.component';


@NgModule({
  declarations: [
    LoginFormComponent
  ],
  imports: [
    CommonModule,
    LoginFormRoutingModule
  ]
})
export class LoginFormModule { }
