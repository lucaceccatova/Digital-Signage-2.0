import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {SliderComponent} from './slider.component';
import { sliderRoutingModule } from './slider-routing.module';
import {GetMediaService} from '../../Services/GetMedia/get-media.service';
import { HttpClientModule } from '@angular/common/http';
import {CommonModule} from '@angular/common';

@NgModule({
  imports: [
    FormsModule,
    sliderRoutingModule,
    HttpClientModule,
    CommonModule,
      
  ],
  declarations: [ SliderComponent ],
  providers:[GetMediaService,]
})
export class SliderModule { 
}
