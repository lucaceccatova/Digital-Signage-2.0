import {Component, Inject} from '@angular/core';
import { MAT_DIALOG_DATA} from '@angular/material/dialog';
import { listMedia } from 'src/app/Model/listMedia';

export interface data
{
  category:listMedia,
   file:string
}

@Component({
  selector: 'categorydialog',
  templateUrl: 'are-you-sure-about-it.component.html',
  styleUrls:['are-you-sure-about-it.component.scss']
})
export class categoryDialog {
  constructor(@Inject(MAT_DIALOG_DATA) public data: data )
  {

  }
}

