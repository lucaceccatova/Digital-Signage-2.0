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
  constructor(private getVideo:GetMediaService, private stream:sharedStringService,private router:Router,private streamElements:shareElementsService) { }


  ngOnInit() {
    //mockup without signalR
    this.loadVideo(this.url);
    
    //this.elements=this.streamElements.elements;
    //this.divideInMorePages();
    
  }  
ngOnDestroy(): void {
  //Called once, before the instance is destroyed.
  //Add 'implements OnDestroy' to the class.
  this.unsubscribes.forEach(element => {
    element.unsubscribe();
  });
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
  sendData(i:number)
  {
    this.stream.data=this.elements[i].path;
    this.stream.time=this.elements[i].timer;
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

}
