import { Component, OnInit } from '@angular/core';
import { media } from 'src/app/Model/media';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';
import { MatDialog } from '@angular/material';
import { UpateDeleteComponent } from '../../category/upate-delete/dialogCategory/upate-delete.component';
import { UpateDeleteMedia } from '../upate-delete/dialogCategory/upate-delete.component';
import { identifierModuleUrl } from '@angular/compiler';
import { apiService } from 'src/app/services/ApiService/Api.service';

@Component({
    selector: 'media-view',
    templateUrl: './mediaView.html',
    styleUrls: ['./mediaView.scss','../../../app.component.scss']
})
export class mediaViewComponent implements OnInit {
    constructor(private api:GetObjectService,private del:apiService ,
        private dialog:MatDialog,
        ) { }

        url="/assets/Json/loadeddata.json";
        urlDel="https://localhost:44303/api/deletemedia";
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
        const dialogTracer=this.dialog.open(UpateDeleteMedia,{
            data:{
                object:this.mediaList[i],
                message:"Are you sure to delete this media?"
            }
        });
        dialogTracer.afterClosed().subscribe(data=>
            {
                if(data=='delete')
                {
                    this.deleteMedia(this.mediaList[i].listId);
                    this.mediaList.splice(i,1);

                }
                else if(data=='update')
                {

                }
            });

      }
      deleteMedia(id:number)
      {
        this.del.get_ID(this.urlDel,id).subscribe(data=>
            {

            });
      }
}
