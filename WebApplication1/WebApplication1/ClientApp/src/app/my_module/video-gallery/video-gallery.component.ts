import { Component, OnInit, AfterViewInit, Output, Injectable } from '@angular/core';
import { GetMediaService } from 'src/app/Services/GetMedia/get-media.service';
import { element } from 'src/app/Models/Element';
import { Subscription } from 'rxjs';
import * as $ from 'jquery';
import { OutgoingMessage } from 'http';
import { EventEmitter } from 'events';
import { Directive } from '@angular/core';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';

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

  constructor(private getVideo:GetMediaService, private stream:sharedStringService,private router:Router) { }


  ngOnInit() {

  this.loadVideo(this.url);
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
    }));
  }

}
