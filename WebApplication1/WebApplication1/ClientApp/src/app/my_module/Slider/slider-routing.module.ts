import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SliderComponent } from './slider.component';
import { ListViewComponent } from './ListView/list-view.component';

const routes: Routes = [
  {
    path: '',
    component: ListViewComponent,
    data: {
      title: 'list'
    }
    
  }
  ,
  {
    path:'media/:id',
    component: SliderComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class sliderRoutingModule {}
