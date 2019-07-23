import { Injectable } from '@angular/core';
import { element } from 'src/app/Models/Element';
import { videoPage } from 'src/app/Models/videoPage';

@Injectable()
export class sharedStringService {
    singleVideo:element;
    pages:videoPage[]=null;
}