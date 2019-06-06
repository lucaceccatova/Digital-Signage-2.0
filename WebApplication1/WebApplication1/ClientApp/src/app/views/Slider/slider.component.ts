import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
@Component({
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent
{
  images = [1, 2, 3].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);


}
  

