import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Form1Component } from './pages/test/forms/form1/form1.component';

const routes: Routes = [
  { path: 'login', loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule) },
  { path: 'test', loadChildren: () => import('./pages/test/two-way-binding/two-way-binding.module').then(m => m.TwoWayBindingModule) },
  { path: 'form', component: Form1Component },
  { path: '', redirectTo: '/login/login-form', pathMatch: 'full' },
  { path: '**', redirectTo: '/login/login-form' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    useHash: true
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
