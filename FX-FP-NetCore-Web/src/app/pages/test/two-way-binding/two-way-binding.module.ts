import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TwoWayBindingRoutingModule } from './two-way-binding-routing.module';
import { TwoWayBindingComponent } from './two-way-binding.component';
import { JichuComponent } from './jichu/jichu.component';
import { FormsModule } from '@angular/forms';
import { ZhilingComponent } from './zhiling/zhiling.component';
import { Zhiling2Component } from './zhiling2/zhiling2.component';
import { HighlightDirective } from './directive/highlight.directive';


@NgModule({
  declarations: [
    TwoWayBindingComponent,
    JichuComponent,
    ZhilingComponent,
    Zhiling2Component,
    HighlightDirective
  ],
  imports: [
    CommonModule,
    TwoWayBindingRoutingModule,
    FormsModule
  ]
})
export class TwoWayBindingModule { }
