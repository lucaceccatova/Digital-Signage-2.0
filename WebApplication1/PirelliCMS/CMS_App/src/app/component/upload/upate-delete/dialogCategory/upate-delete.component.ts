import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface data
{
  object:any,
  message:string,
}

@Component({
  selector: 'update-delete-media',
  templateUrl: './upate-delete.component.html',
  styleUrls: ['./upate-delete.component.scss']
})
export class UpateDeleteMedia {

  constructor(@Inject(MAT_DIALOG_DATA) public data: data )
   { 

   }
}
