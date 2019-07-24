import { Component, OnInit, AfterViewInit, Output, Injectable } from '@angular/core';
import { GetMediaService } from 'src/app/Services/GetMedia/get-media.service';
import { element } from 'src/app/Models/Element';
import { Subscription } from 'rxjs';
import $ from 'jquery';
import { Router } from '@angular/router';
import {videoPage} from 'src/app/Models/videoPage';
import { shareElementsService } from 'src/app/Services/sharedServices/shareElement.Service';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { SignalRService } from 'src/app/Services/sharedServices/signal-r.service';
import { tireShareService } from 'src/app/Services/sharedServices/shareTireService';
@Component({
  selector: 'app-video-gallery',
  templateUrl: './video-gallery.component.html',
  styleUrls: ['./video-gallery.component.scss'],
})
export class VideoGalleryComponent implements OnInit {
  //api url
 // url:string="/assets/loadeddata.video.json";
  //model for video
  elements:element[]=[];
  unsubscribes: Subscription[]=[];
  pages:videoPage[]=[];
  indexPage=0;
//bool to reload the page
  reload=true;

  constructor(private getVideo:GetMediaService, private stream:sharedStringService,
    private router:Router,private streamElements:shareElementsService,
    private connectionService:SignalRService,
    private tireStream:tireShareService) { 
    }

  ngOnInit() {
    //mockup without signalR
    //this.loadVideo(this.url);
    //db
    
    if(this.stream.pages!=null)
    {
      this.pages=this.stream.pages;
      this.stream.pages=null;
    }
    else{
      this.elements=this.streamElements.elements;
      this.divideInMorePages();
    }
    //contains function invoked by signalr
   this.signalRListner();
}

//timer that navigate after x seconds of inactivity


ngOnDestroy(): void {
  //Called once, before the instance is destroyed.
  //Add 'implements OnDestroy' to the class.
  this.unsubscribes.forEach(element => {
    element.unsubscribe();
  });
  this.connectionService.connection.off("showVideo");
}


//signalR directives
signalRListner()
{
  //to play one video fullscreen
  this.connectionService.connection.on('showVideo',(data)=>
  {
    if(data<=this.pages[this.indexPage].sixElements.length)
      {
        this.sendData(this.pages[this.indexPage].sixElements[data-1]);
      }
  });
  //change wich videos are displayed on the gallery 
  this.connectionService.connection.on('showVideoGallery',(data)=>
  {
    this.pages=[];
    this.elements=data;
    this.divideInMorePages();
    this.reloadNgFor();
  });
  //pass to show-tire 
  this.connectionService.connection.on("tireShow",(data)=>
  {
    this.tireStream.tires=data;
    console.log(this.tireStream.tires);
    console.log(data);
    this.connectionService.connection.off("tireShow");
    this.router.navigateByUrl('/tire');
  });

  //switch page if invoked by backend. (why not a bool :(  )
  this.connectionService.connection.on('receivePage',(data)=>
  {
    if(data=="next")
    {
      this.nextPage();
    }
    else{
      this.prevPage();
    }
  });
}

//to divide a json that contains more than 6 videos in object with six or less video
divideInMorePages()
{
  let i:number=0,k:number;
  let videoPage:videoPage={sixElements:[]};
  while(i<this.elements.length)
  {
    for(k=0;k<6;k++)
    {
      if(this.elements[i]!=null)
      {
        
        videoPage.sixElements.push(this.elements[i]);
        i++;
      }
      else break;
    }
    this.pages.push(videoPage);
    videoPage:videoPage={sixElements:[]};
  }
  console.log(this.pages);
}


  //service that pass the path to fsVideo component
  //without expose the Url in the Url 
  sendData(i:element)
  {
    this.stream.pages=this.pages;
    this.stream.singleVideo=i;
    console.log(i);
    this.router.navigateByUrl("/video/media");
  }

  //mockup function that take data from web api
  loadVideo(url:string)
  {
   this.unsubscribes.push(this.getVideo.get(url).subscribe(data=>
    {
      this.elements=data;
      this.divideInMorePages();
    }));
  }

  //refresh ngfor :(
  reloadNgFor()
  {
    this.reload=false;
    setTimeout(() => {
      this.reload=true;
    }, 500);
  }

  //to switch video page to next video page
  nextPage()
  {
     if(this.indexPage<this.pages.length-1)
    {
      $("#page"+(this.indexPage)).animate({left:'-100%'},750);
      setTimeout(() => {
        this.indexPage++;
      $("#page"+this.indexPage).animate({left: '0'},750);
      $("#page"+(this.indexPage-1)).animate({left:'100%'},0);

    }, 750);

    }
    //in case there are no next page video gallery will show first video page
    else{
      $("#page"+this.indexPage).animate({left:'-100%'},750);
      setTimeout(() => {
      $("#page"+this.indexPage).animate({left:'100%'},0);
        this.indexPage=0;
      $("#page"+this.indexPage).animate({left: '0'},750);
      }, 750);
    }
  }
  //to switch video page to previous video page
  prevPage()
  {
     if(this.indexPage!=0)
    {
      $("#page"+this.indexPage).animate({left:'100%'},750);
      setTimeout(() => {
        this.indexPage--;
      $("#page"+this.indexPage).animate({left: '-100%'},0);
      $("#page"+(this.indexPage)).animate({left:'0'},750);
      }, 750);
    }

    //in case there are no previous pages the slider will show last page
    else{
      $("#page"+(this.indexPage)).animate({left:'100%'},750);
      setTimeout(() => {
        this.indexPage=this.pages.length-1;
      $("#page"+this.indexPage).animate({left:'-100%'},0);
      $("#page"+this.indexPage).animate({left: '0'},750);
      }, 750);
    }
  }

  //trying to move logic from dom to typescript for better maintainence
  selected<bool>(index:number)
    {
      if(this.indexPage==index)
        return true;
        else
        return false; 
    }
}
