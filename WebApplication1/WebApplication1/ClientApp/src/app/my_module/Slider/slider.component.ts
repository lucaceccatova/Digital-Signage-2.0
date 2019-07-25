import { Component, OnInit ,OnDestroy, Output, Input} from '@angular/core';
import { Router } from '@angular/router';
import {GetMediaService} from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable, Subscription, from, Observable, config } from 'rxjs';
import { ViewEncapsulation } from '@angular/core';
import {CommonModule} from "@angular/common";
import {ActivatedRoute} from '@angular/router';
import { Routes } from '@angular/router';
import $ from 'jquery';
import { shareElementsService } from 'src/app/Services/sharedServices/shareElement.Service';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
import { ShareService } from 'src/app/Services/sharedServices/universalShareService';
import { environment } from 'src/environments/environment';
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { tireShareService } from 'src/app/Services/sharedServices/shareTireService';

@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})

export class SliderComponent implements OnInit,OnDestroy
{
  //url:string="/assets/loadeddata.json";
url:string=environment.baseUrl+"api/getdati";
  public elements:element[];
  unsubscribes: Subscription[]=[];
  //startingSlide is the index of the media displayed in slider
  startingSlide=0;
  //id, server directive for showing a specific slide 
  serviceInt:number;
  newDirective:boolean;
  //observable for newDirective
  indexInterrup:number=0;
  //set timeout 
  setTimeoutInterceptor;
  //signalR connection object
  MyId:number;
  temp;
  // this is the id of the gallery that will be displayed,
  //http get will have a int param
constructor(private http:GetMediaService,private _Activatedroute:ActivatedRoute,private router : Router,private streamElements:shareElementsService,
  private connectionService:SignalRService,
  private UniversalShare:ShareService,
  private tireStream:tireShareService)
{
  
}
  //#region init
    ngOnInit()
    {
      //obsolete, read param from URL for gallery ID
      this.MyId=(Number).parseInt(this._Activatedroute.snapshot.paramMap.get("id"));
      //take data by a HTTP get request to url saved in url string 
      this.getData();
      //
      this.signalRConnection();    
    }
  //#endregion
  //#region destroy
    //to keep application lightweight also after long session
    ngOnDestroy()
    {
      this.unsubscribes.forEach(element => {
        element.unsubscribe();
      });
      this.stopEngine();
    }
  //#endregion
  //#region sliderEngine 
    //recursive function that display a slide by changing variable starting slide
    slideEngine()
    {
      if (this.elements[this.startingSlide].format == 0) {
        this.playVideoFromId("vid" + this.startingSlide);
      }
        this.setTimeoutInterceptor=setTimeout(() => {
          //if slides are ended restart from zero
          if(this.elements.length>(this.startingSlide+1))
          {
            //fadeOut all the box then change img inside slider
            $("#mySliderBox").fadeOut(500);
            setTimeout(() => {
              this.startingSlide++;
              this.slideEngine();
            }, 500);
            //fadeIn the bo with new img
            $("#mySliderBox").fadeIn(500);
            //to start video when is displayed in the slider
            //if(this.elements[this.startingSlide].value==0)
            //    this.playVideoFromId("vid"+this.startingSlide);    
          }
          else
          {
            $("#mySliderBox").fadeOut(500);
            setTimeout(() => {
              this.startingSlide=0;
              this.slideEngine();
            }, 500);
          
            $("#mySliderBox").fadeIn(500);
          }
          
        }, this.elements[this.startingSlide].timer*1000);
      }
      //to stop slider when leaving the component
      stopEngine()
      {
        clearTimeout(this.setTimeoutInterceptor);
      }
  //#endregion
  //#region signalR directives
    //function that estabilish a connection withe backend service
    signalRConnection(){  
      this.connectionService.connection.on("showVideoGallery", (data)=>
      {
        //call service that send data to video gallery component
        this.streamElements.elements=data;
        
        //ng route to video gallery component
        this.stopEngine();
        this.router.navigateByUrl("/video")
      });
      //pass to show-tire 
      this.connectionService.connection.on("tireShow",(data)=>
      {
        this.tireStream.tires=data;
        this.connectionService.connection.off("tireShow");
        this.router.navigateByUrl('/tire');
      });
      this.connectionService.connection.on("showCarTires",data=>
      {
        this.UniversalShare.sharedObject=data;
        this.stopEngine();
        this.router.navigateByUrl("/tire");
      });
      
    }
  //#endregion
  //#region utilities
    //start a video
    playVideoFromId(id:string)
    {
      let vid = <HTMLVideoElement>document.getElementById(id);
      vid.play();
    }
  //#endregion
  //#region dom logic
  //trying to move logic from dom to typescript
  visible(index:number)
  {
    if(index==this.startingSlide)
    return true;
    else 
      return false;
  } 
//#endregion 
  //#region mockup
      //get json from BE and deserialize it into an array of element
      getData()
      {
        this.unsubscribes.push(this.http.get(this.url).subscribe(data =>
            {
              this.elements=data;
              this.slideEngine();
            })  
          );
      }
    //#endregion   
}