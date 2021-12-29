import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { zh_CN } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import zh from '@angular/common/locales/zh';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedModule } from './shared/shared.module';

import { TwoWayBindingModule } from './pages/test/two-way-binding/two-way-binding.module';
import { Form1Component } from './pages/test/forms/form1/form1.component';
import { TransferPanelComponent } from './pages/test/transfer/transfer-panel/transfer-panel.component';
import { TransferPageComponent } from './pages/test/transfer/transfer-page/transfer-page.component';


registerLocaleData(zh);

@NgModule({
  declarations: [
    AppComponent,
    Form1Component,
    TransferPanelComponent,
    TransferPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    TwoWayBindingModule
  ],
  providers: [{ provide: NZ_I18N, useValue: zh_CN }],
  bootstrap: [AppComponent]
})
export class AppModule { }
