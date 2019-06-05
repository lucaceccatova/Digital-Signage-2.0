import { Component, OnInit } from '@angular/core';
import { element } from '../../Model/element';
import { from, Observable } from 'rxjs';
import {JsonService} from '../../services/json.service';
@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements OnInit {
  images = [1, 2, 3].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);
  temp:Observable<any>;
  
  constructor(
    private json:JsonService
    ) {


  }
  GetJson():Observable<any>
    {
      return this.json.getJSON();
    }
  ngOnInit(){
      this.temp=this.GetJson();
    }
  }


