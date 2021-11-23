import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DefaultComponent } from './default/default.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ListCatsComponent } from './cats/list-cats/list-cats.component';
import { ListUsersComponent } from './Users/list-users/list-users.component';

const routes: Routes = [
  // { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'default', component: DefaultComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'cats', component: ListCatsComponent },
  { path: 'users', component: ListUsersComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
