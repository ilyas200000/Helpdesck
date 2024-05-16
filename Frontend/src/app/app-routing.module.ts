import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authentification/login/login.component';
import { HomeComponent } from './shared_component/home/home.component';
import { authGuard } from './authentification/_helpers/auth.guard';
import { AccessDeniedComponent } from './shared_component/errors/access-denied/access-denied.component';
import {AdminComponent} from "./admin/admin.component";

const routes: Routes = [
  {
    path: '',
    redirectTo:'/home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component:LoginComponent
  },
  {
    path: 'home',
    component: HomeComponent ,
    canActivate: [authGuard],
    data: {roles: ['Admin','User']}
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [authGuard],
    data: {roles: ['Admin']}
  },
  {
    path: 'forbidden',
    component: AccessDeniedComponent
  },
  {
    path: '**',
    redirectTo: '/home'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
