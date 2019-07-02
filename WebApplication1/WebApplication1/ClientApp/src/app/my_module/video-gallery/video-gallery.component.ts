import { Component, OnInit, AfterViewInit, Output, Injectable } from '@angular/core';
import { GetMediaService } from 'src/app/Services/GetMedia/get-media.service';
import { element } from 'src/app/Models/Element';
import { Subscription } from 'rxjs';
import * as $ from 'jquery';
import { OutgoingMessage } from 'http';
import { EventEmitter } from 'events';
import { Directive } from '@angular/core';
import { Router } from '@angular/router';
import {videoPage} from 'src/app/Models/videoPage';
import { shareElementsService } from 'src/app/Services/shareElementsServie/shareElement.Service';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
@Component({
  selector: 'app-video-gallery',
  templateUrl: './video-gallery.component.html',
  styleUrls: ['./video-gallery.component.scss'],
})
export class VideoGalleryComponent implements OnInit {
  //api url
  url:string="/assets/loadeddata.video.json";
  //model for video
  elements:element[]=[];
  unsubscribes: Subscription[]=[];
  pages:videoPage[]=[];
//bool to reload the page
  reload=true;

  constructor(private getVideo:GetMediaService, private stream:sharedStringService,
    private router:Router,private streamElements:shareElementsService,
    private connectionService:SignalRService) { 
      
    }


  ngOnInit() {
    //mockup without signalR
    this.loadVideo(this.url);
    
    //this.elements=this.streamElements.elements;
    //this.divideInMorePages();
    
    this.signalRListner();

    //timeoutthat return to slider after 3 minutes
   
}
returnBackTimer()
{
  setTimeout(() => {
    this.router.navigateByUrl('/slider');
  }, 2000);

}
ngOnDestroy(): void {
  //Called once, before the instance is destroyed.
  //Add 'implements OnDestroy' to the class.
  this.unsubscribes.forEach(element => {
    element.unsubscribe();
  });
}
//signalR directives
signalRListner()
{
  this.connectionService.connection.on('showVideo',(data)=>
  {
    this.sendData(data);
  });
  this.connectionService.connection.on('showVideoGallery',(data)=>
  {
    this.elements=data;
    this.reloadNgFor();
    this.router.navigateByUrl('/video');
  });
  this.connectionService.connection.on('goToSlide',(data=>
    {
      if(data==true)
      {
        
        this.router.navigateByUrl("/slider/media/1");
      }
    }));
}
divideInMorePages()
{
  let i:number=0,k:number;
  let videoPage:videoPage={sixElements:[]};
  while(i<this.elements.length-1)
  {
    for(k=0;k<6;k++)
    {
      if(this.elements[i]!=null)
      {
        videoPage.sixElements.push(this.elements[i]);
        i++;
      }
      else k=58;
    }
    this.pages.push(videoPage);
    videoPage=null;
  }
}

  //service that pass the path to fsVideo component
  //without expose the Url in the Url 
  sendData(i:element)
  {
    this.stream.singleVideo=i;
    this.router.navigateByUrl("/video/media");
  }

  loadVideo(url:string)
  {
   this.unsubscribes.push(this.getVideo.get(url).subscribe(data=>
    {
      this.elements=data;
      this.divideInMorePages();
    }));
  }
  reloadNgFor()
  {
    this.reload=false;
    setTimeout(() => {
      this.reload=true;
    }, 500);
  }
}
