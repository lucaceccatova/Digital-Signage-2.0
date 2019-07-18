import { Component, OnInit } from '@angular/core';
import { media } from 'src/app/Model/media';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';
import { MatDialog } from '@angular/material';
import { UpateDeleteComponent } from '../../category/upate-delete/dialogCategory/upate-delete.component';
import { UpateDeleteMedia } from '../upate-delete/dialogCategory/upate-delete.component';

@Component({
    selector: 'media-view',
    templateUrl: './mediaView.html',
    styleUrls: ['./mediaView.scss','../../../app.component.scss']
})
export class mediaViewComponent implements OnInit {
    constructor(private api:GetObjectService, private dialog:MatDialog) { }
    url="/assets/Json/loadeddata.json";
    mediaList:media[]=[];
    ngOnInit(): void { 
        this.getmedia();
    }

    getmedia()
    {
        this.api.getMedia(this.url).subscribe(data=>
            {
                this.mediaList=data;
                console.log(this.mediaList);
            });
    }

    trackByFn(i: number) { 
        return i;
      }

    dialogInvoke(i:number)
      {
        this.dialog.open(UpateDeleteMedia,{
            data:{
                object:this.mediaList[i],
                message:"Are you sure to delete this media?"
            }
            
        })
      }
}
