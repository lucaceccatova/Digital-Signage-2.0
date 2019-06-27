import { Component, OnInit ,OnDestroy, Output, Input} from '@angular/core';
import { Router } from '@angular/router';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {ServerListnerService} from '../../Services/Listner/server-listner.service';
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
@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})

export class SliderComponent implements OnInit,OnDestroy
{
 
  url:string="/assets/loadeddata.json";
 // url:string="https://localhost:44303/api/test/getlistaById"
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
  private connection : HubConnection;
  MyId:number;
  temp;
  // this is the id of the gallery that will be displayed,
  //http get will have a int param
constructor(private http:GetMediaService,private dir:ServerListnerService,private _Activatedroute:ActivatedRoute,private router : Router)
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
  this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
  .configureLogging(signalR.LogLevel.Information)
  .build();
    this.connection
    .start()
    .then(()=>console.log("connection started"))
    .catch(err=>console.log("Errore di connessione"));


    this.connection.on("ReceiveMessage", (n)=>
    {
      if(n<this.elements.length&&n>=0)
      {
        this.startingSlide=n;
        clearTimeout(this.setTimeoutInterceptor);
        this.slideEngine();
      }
      else{
        console.log("Direttiva errata da backend");
      }
    });
  }


  //method that listen to signalR messages from backend and verify them
  signalRDirectiveListener(){
    //provvisory: invoke signalr method 
    this.connection
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

