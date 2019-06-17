

import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

import {SliderComponent} from './slider.component';
import { sliderRoutingModule } from './slider-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {GetMediaService} from '../../Services/GetMedia/get-media.service';
import {ListService} from '../../Services/ListService/list-service.service';
import { HttpClientModule } from '@angular/common/http';
import {CommonModule} from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import {ServerListnerService} from '../../Services/Listner/server-listner.service';
import {ListViewComponent} from "./ListView/list-view.component";
@NgModule({
  imports: [
    FormsModule,
    sliderRoutingModule,
    ChartsModule,
    BsDropdownModule,
    ButtonsModule.forRoot(),
    NgbModule,
    HttpClientModule,
    CommonModule,
    CarouselModule,
    
    
    
    
    
  ],
  declarations: [ SliderComponent,ListViewComponent ],
  providers:[GetMediaService,ServerListnerService,ListService]
})
export class SliderModule { 

  next()
  {
    
  }

}
