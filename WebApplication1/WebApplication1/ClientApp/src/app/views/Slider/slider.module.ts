

import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

import {SliderComponent} from './slider.component';
import { sliderRoutingModule } from './slider-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  imports: [
    FormsModule,
    sliderRoutingModule,
    ChartsModule,
    BsDropdownModule,
    ButtonsModule.forRoot(),
    NgbModule
    
  ],
  declarations: [ SliderComponent ]
})
export class SliderModule { 

  next()
  {
    
  }

}
