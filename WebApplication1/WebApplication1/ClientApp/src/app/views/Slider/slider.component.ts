import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable } from 'rxjs';
@Component({
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent implements OnInit
{
  images = [1, 2, 3].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);
  elements:element[];
constructor(private getJson:GetMediaService)
{
  this.ParseData();
  console.log(this.elements);
}
ngOnInit()
{
  this.getJson.GetJSON().subscribe
  (
    data=>
    {
      this.elements=JSON.parse(data.toString());
    }
  )
}
  ParseData(){
 
  }
}

