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
import { shareElementsService } from 'src/app/Services/shareElementsServie/shareElement.Service';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
import { ShareService } from 'src/app/Services/UniversalShare/universalShareService';
@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})

export class SliderComponent implements OnInit,OnDestroy
{
  //url:string="/assets/loadeddata.json";
url:string="https://localhost:44303/api/test/getdati"
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
  private connectionService:SignalRService,private UniversalShare:ShareService)
{
  
}

  
ngOnInit()
{
  //obsolete, read param from URL for gallery ID
  this.MyId=(Number).parseInt(this._Activatedroute.snapshot.paramMap.get("id"));
  //take data by a HTTP get request to url saved in url string 
  this.getDataMock();
  //
  this.signalRConnection();    
}


//to keep application lightweight also after long session
ngOnDestroy()
{
  this.unsubscribes.forEach(element => {
    element.unsubscribe();
  });
}


  //get json from BE and deserialize it into an array of element
 getDataMock()
 {
   console.log(this.MyId);
  this.temp=this.url+'/'+this.MyId.toString();
   this.unsubscribes.push(this.http.get(this.url).subscribe(data =>
      {
        this.elements=data;
        this.slideEngine();
      })  
    );
 }

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
    this.connectionService.connection.on("showVideo",data=>
    {
      //show one single video
    });
    this.connectionService.connection.on("showCarTires",data=>
    {
      this.UniversalShare.sharedObject=data;
      this.stopEngine();
      this.router.navigateByUrl("/tire");
    });
    
  }

  //to stop slider when leaving the component
  stopEngine()
  {
    clearTimeout(this.setTimeoutInterceptor);
  }


  //method that listen to signalR messages from backend and verify them
  signalRDirectiveListener(){
    //provvisory: invoke signalr method 
    this.connectionService.connection
    .invoke('SendMessage', 2)
    .catch(err => console.error(err));
    //what happens whe ReceiveMessage signalR function is invoked 
  }
  
  playVideoFromId(id:string)
  {
    console.log(id);
    let vid = <HTMLVideoElement>document.getElementById(id);
    console.log(vid);
    //uncomment after vs start
    vid.play();
  }
  
  //trying to move logic from dom to typescript
  visible(index:number)
  {
    if(index==this.startingSlide)
    return true;
    else 
      return false;
  }  
}

