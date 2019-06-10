import { Component, OnInit ,OnDestroy} from '@angular/core';

import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable, Subscription, from, Observable, config } from 'rxjs';
import { ViewEncapsulation } from '@angular/core'
import {NgbCarouselConfig} from '@ng-bootstrap/ng-bootstrap';

import { map } from 'rxjs/operators';
import { getLocaleDateFormat } from '@angular/common';
import {CommonModule} from "@angular/common";
import { compileBaseDefFromMetadata } from '@angular/compiler';
import {CarouselsComponent} from '../../views/base/carousels.component';

@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent implements OnInit
{
  url:string="/assets/loadeddata.json"
  elements:element[];
  unsubscribes: Array<Subscription>;
  
constructor(private http:GetMediaService, private config:NgbCarouselConfig)
{
 
}
ngOnInit():void
{
  this.getDataMock();

  
}

 getDataMock()
 {
   
  this.http.get(this.url).subscribe(data=>
    {
      this.elements=data;
    })  
 }
}
