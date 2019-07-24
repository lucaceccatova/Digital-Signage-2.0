import { Component, OnInit } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { element } from 'src/app/Models/Element';
import { tireShareService } from '../../Services/sharedServices/shareTireService';
import { SignalRService } from 'src/app/Services/sharedServices/signal-r.service';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';
import { preserveSpecService } from 'src/app/Services/preserveSpecComponent/preserveSpec.service';

@Component({
    selector: 'tire-spec',
    templateUrl: './tire-specs.component.html',
    styleUrls: ['./tire-specs.component.scss']
})
export class tireSpecsComponent implements OnInit {
    constructor(private dataStream:tireShareService,
      private connection:SignalRService,
      private video:sharedStringService,
      private router:Router,
      private backUp:preserveSpecService) { }
    tire:tire; selectedVideo:number;
    videos:element[]=[];
    ngOnInit(): void { 
        //this.mock();

      if(this.backUp.video!=null)
        this.restore();
      else
        this.getData();

      this.signalRListnter();
    }
    ngOnDestroy(): void {
      //Called once, before the instance is destroyed.
      //Add 'implements OnDestroy' to the class.
      this.connection.connection.off("showVideo");

    }

    mock()
    {
        this.tire={
                    id:0,
                    model:"Pzero",
                    tirePath:"/assets/img/tireShow.jpg",
                    tireType:"Pista",
                    price:200,
                    size:21
      }
      this.videos.push({
        id: 4, name: "Pirelli Girl", description: "Una bella città", format: 0, timer: 15, create_date: "2019-07-06T00:00:00", path: "assets/Movie/pirelliGirl.mp4", ListaID: 1
      },
        {
          id: 4, name: "Pirelli Girl", description: "Una bella città", format: 0, timer: 15, create_date: "2019-07-06T00:00:00", path: "assets/Movie/pirelliGirl.mp4", ListaID: 1
        }, {
          id: 4, name: "Pirelli Girl", description: "Una bella città", format: 0, timer: 15, create_date: "2019-07-06T00:00:00", path: "assets/Movie/pirelliGirl.mp4", ListaID: 1
        }
      );
  }
  getData() {
    this.tire = this.dataStream.selectedTire;
    this.videos = this.dataStream.videos;
  }
  signalRListnter()
  {
    this.connection.connection.on("showVideo",data=>{
      this.video.singleVideo=this.videos[data-1];
      this.save();
      this.router.navigateByUrl("/video/media");
    });
  }
  restore()
  {
    this.tire=this.backUp.selectedTire;
    this.videos=this.backUp.video;
    this.backUp.reset();
  }
  save()
  {
    this.backUp.video=this.videos;
    this.backUp.selectedTire=this.tire;
  }
}
