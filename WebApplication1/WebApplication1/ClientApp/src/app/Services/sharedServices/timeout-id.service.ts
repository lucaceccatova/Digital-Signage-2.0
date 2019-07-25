import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimeoutIdService {
  constructor() { }
  animationTimeOut:NodeJS.Timer[]=[];
  videoTimeout:NodeJS.Timer=null;
  
}
