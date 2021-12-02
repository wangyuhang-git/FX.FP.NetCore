import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login.component';

const routes: Routes = [
  {
    path: '', data: { key: 'login', shouldDetach: 'no' }, component: LoginComponent, children: [
      {
        path: 'login-form',
        loadChildren: () => import('./login-form/login-form.module').then(m => m.LoginFormModule)
      },
      {
        path: 'register-form',
        loadChildren: () => import('./register-form/register-form.module').then(m => m.RegisterFormModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
