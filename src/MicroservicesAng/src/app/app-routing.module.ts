import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MPCv1Component } from './mpcv1/mpcv1.component';

const routes: Routes = [
  {path: '', redirectTo: '/static/mpcv1', pathMatch: 'full'},
  {path: 'static/mpcv1', component: MPCv1Component},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
