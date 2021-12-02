import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RegisterFormRoutingModule } from './register-form-routing.module';
import { RegisterFormComponent } from './register-form.component';


@NgModule({
  declarations: [
    RegisterFormComponent
  ],
  imports: [
    CommonModule,
    RegisterFormRoutingModule
  ]
})
export class RegisterFormModule { }
