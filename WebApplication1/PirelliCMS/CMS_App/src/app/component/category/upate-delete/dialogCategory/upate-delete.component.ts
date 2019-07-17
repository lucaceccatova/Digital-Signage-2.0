import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA} from '@angular/material/dialog';
import { listMedia } from 'src/app/Model/listMedia';

export interface data
{
  category:listMedia,
}

@Component({
  selector: 'app-upate-delete',
  templateUrl: './upate-delete.component.html',
  styleUrls: ['./upate-delete.component.scss']
})
export class UpateDeleteComponent {

  message="Are you sure to delete this category?";
  constructor(@Inject(MAT_DIALOG_DATA) public data: data )
   { }
}
