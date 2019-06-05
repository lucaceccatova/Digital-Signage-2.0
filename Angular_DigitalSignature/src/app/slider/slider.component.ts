import { Component, OnInit } from '@angular/core';
import { element } from '../Model/element';
import { Variable } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {
  images = [1, 2, 3].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);


  constructor() {
   }


  ngOnInit() {
  }

}
