import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent implements OnInit
{
  images = [1, 2, 3].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);
  elements:element[]=[];
  
constructor(private getJson:GetMediaService)
{
}
ngOnInit()
{
this.getJS()
console.log(this.elements);
}

getJS() {
  let x;
  this.getJson.GetJSON().pipe(
    map(
      x=> this.elements=x
    )
  )
  
    .subscribe();
}
}
