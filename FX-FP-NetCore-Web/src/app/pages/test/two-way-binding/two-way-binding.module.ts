import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TwoWayBindingRoutingModule } from './two-way-binding-routing.module';
import { TwoWayBindingComponent } from './two-way-binding.component';
import { JichuComponent } from './jichu/jichu.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    TwoWayBindingComponent,
    JichuComponent
  ],
  imports: [
    CommonModule,
    TwoWayBindingRoutingModule,
    FormsModule
  ]
})
export class TwoWayBindingModule { }
