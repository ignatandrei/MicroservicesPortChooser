import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MPCv1Component } from './mpcv1/mpcv1.component';

import {  Mpcv2Component } from './mpcv2/mpcv2.component';
import { RegisteredMPCComponent } from './registered-mpc/registered-mpc.component';

const routes: Routes = [
  {path: '', redirectTo: '/static/mpcv1', pathMatch: 'full'},
  {path: 'static/mpcv1', component: MPCv1Component},
  {path: 'static/mpcv2', component:  Mpcv2Component},
  {path: 'static/services', component:  RegisteredMPCComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
