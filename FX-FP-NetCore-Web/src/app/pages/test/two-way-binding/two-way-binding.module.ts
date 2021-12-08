import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TwoWayBindingRoutingModule } from './two-way-binding-routing.module';
import { TwoWayBindingComponent } from './two-way-binding.component';
import { JichuComponent } from './jichu/jichu.component';


@NgModule({
  declarations: [
    TwoWayBindingComponent,
    JichuComponent
  ],
  imports: [
    CommonModule,
    TwoWayBindingRoutingModule
  ]
})
export class TwoWayBindingModule { }
