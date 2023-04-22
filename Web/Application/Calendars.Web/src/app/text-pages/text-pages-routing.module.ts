import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { InfoComponent } from './info/info.component';
import { NotFound404Component } from './not-found404/not-found404.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
},
{
    path: 'info',
    component: InfoComponent
},
{
  path: '**',
  component: NotFound404Component
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class TextPagesRoutingModule { }
