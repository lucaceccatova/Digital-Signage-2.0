import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {SliderComponent} from './slider.component';
import { sliderRoutingModule } from './slider-routing.module';
import {GetMediaService} from '../../Services/GetMedia/get-media.service';
import {ListService} from '../../Services/ListService/list-service.service';
import { HttpClientModule } from '@angular/common/http';
import {CommonModule} from '@angular/common';
import {ListViewComponent} from "./ListView/list-view.component";

@NgModule({
  imports: [
    FormsModule,
    sliderRoutingModule,
    HttpClientModule,
    CommonModule,  
  ],
  declarations: [ SliderComponent,ListViewComponent ],
  providers:[GetMediaService,ListService]
})
export class SliderModule { 
}
