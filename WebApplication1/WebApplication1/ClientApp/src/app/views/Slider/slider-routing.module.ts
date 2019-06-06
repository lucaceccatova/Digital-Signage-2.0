import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SliderComponent } from './slider.component';

const routes: Routes = [
  {
    path: '',
    component: SliderComponent,
    data: {
      title: 'Slider'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class sliderRoutingModule {}
