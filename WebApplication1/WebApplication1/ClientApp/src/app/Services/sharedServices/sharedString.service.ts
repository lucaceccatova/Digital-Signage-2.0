import { Injectable } from '@angular/core';
import { element } from 'src/app/Models/Element';
import { videoPage } from 'src/app/Models/videoPage';

@Injectable(
    {
        providedIn:'root',
    }
)
export class sharedStringService {
    singleVideo:element;
    pages:videoPage[]=null;
    index:number;
    
    reset()
    {
        this.singleVideo=null;
        this.pages=null;
        this.index=null;
    }
}