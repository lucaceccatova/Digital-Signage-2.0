import { Component, OnInit } from '@angular/core';
import { media } from 'src/app/Model/media';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';

@Component({
    selector: 'media-view',
    templateUrl: './mediaView.html',
    styleUrls: ['./mediaView.scss','../../../app.component.scss']
})
export class mediaViewComponent implements OnInit {
    constructor(private api:GetObjectService) { }
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
}
