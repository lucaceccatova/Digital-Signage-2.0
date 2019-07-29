import { Injectable } from '@angular/core';
import { element } from 'src/app/Models/Element';
import { tire } from 'src/app/Models/tire';

@Injectable(
    {providedIn:'root',}

)
export class preserveSpecService {
    video:element[]=[];
    selectedTire:tire;

    reset()
    {
        this.video=[];
        this.selectedTire;
    }
}