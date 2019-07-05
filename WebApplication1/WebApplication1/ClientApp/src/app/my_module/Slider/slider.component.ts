import { Component, OnInit ,OnDestroy, Output, Input} from '@angular/core';
import { Router } from '@angular/router';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable, Subscription, from, Observable, config } from 'rxjs';
import { ViewEncapsulation } from '@angular/core'
import { getLocaleDateFormat } from '@angular/common';
import {CommonModule} from "@angular/common";
import { compileBaseDefFromMetadata, templateJitUrl } from '@angular/compiler';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { stringify } from '@angular/compiler/src/util';
import { start } from 'repl';
import { Variable } from '@angular/compiler/src/render3/r3_ast';
import {ActivatedRoute} from '@angular/router';
import { Routes } from '@angular/router';
import * as $ from 'jquery';
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
 url:string="https://localhost:44303/api/test/getlistaById"
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
  this.MyId=(Number).parseInt(this._Activatedroute.snapshot.paramMap.get("id"));
  this.getDataMock();
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
   this.unsubscribes.push(this.http.get(this.temp).subscribe(data =>
    {
      this.elements=data;
      this.slideEngine();
     

    })  
    );
 }

  //recursive function that display a slide by changing variable starting slide
slideEngine()
  {
    this.setTimeoutInterceptor=setTimeout(() => {
      //if slides are ended restart from zero
      //idea: switch slides gallery when one end
       if(this.elements.length>this.startingSlide+1)
      {
        this.startingSlide++;
        //to start video when is displayed in the slider
        if(this.elements[this.startingSlide].value==0)
            this.playVideoFromId("vid"+this.startingSlide);    
      }
      else
      {
        this.startingSlide=0;
      }
      this.slideEngine();
    }, this.elements[this.startingSlide].timer*1000);
  }


  //function that estabilish a connection withe backend service
  signalRConnection(){  
    this.connectionService.connect();
    this.connectionService.connection.on("showVideoGallery", (data)=>
    {
      //call service that send data to video gallery component
      this.streamElements.elements=data;
    
      //ng route to video gallery component
      this.router.navigateByUrl("/video")
    });
    this.connectionService.connection.on("showVideo",data=>
    {
      //show one single video
    });
    this.connectionService.connection.on("showCarTires",data=>
    {
      this.UniversalShare.sharedObject=data;
      this.router.navigateByUrl("/tire");
    })
  
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
    let video=document.getElementById(id);
   // video.play();
  }
}

/*if(what=="goSlide")
      {
        if(how<this.elements.length&&how>=0)
        {
          this.startingSlide=how;
          clearTimeout(this.setTimeoutInterceptor);
          this.slideEngine();
        }
        else{
          console.log("Direttiva errata da backend");
        }
      }*/

