import { Injectable } from '@angular/core';
import { element } from 'src/app/Models/Element';
import { tire } from 'src/app/Models/tire';

@Injectable(
)
export class preserveSpecService {
    video:element[]=null;
    selectedTire:tire=null;

    reset()
    {
        this.video=null;
        this.selectedTire=null;
    }
}