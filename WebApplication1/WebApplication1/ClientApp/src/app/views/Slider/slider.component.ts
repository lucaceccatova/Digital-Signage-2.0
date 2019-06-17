import { Component, OnInit ,OnDestroy, Output, Input} from '@angular/core';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {ServerListnerService} from '../../Services/Listner/server-listner.service';
import {element} from '../../Models/Element';
import { observable, Subscription, from, Observable, config } from 'rxjs';
import { ViewEncapsulation } from '@angular/core'
import { getLocaleDateFormat } from '@angular/common';
import {CommonModule} from "@angular/common";
import { compileBaseDefFromMetadata } from '@angular/compiler';
import {CarouselsComponent} from '../../views/base/carousels.component';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { stringify } from '@angular/compiler/src/util';
import { start } from 'repl';
import { Variable } from '@angular/compiler/src/render3/r3_ast';
import {ActivatedRoute} from '@angular/router';
import { Routes } from '@angular/router';
import { TouchSequence } from 'selenium-webdriver';

@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})

export class SliderComponent implements OnInit,OnDestroy
{
 
  url:string="/assets/loadeddata.json";
  //url:string="https://localhost:44303/api/test/getdati"
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
constructor(private http:GetMediaService,private dir:ServerListnerService,private _Activatedroute:ActivatedRoute,private router : Router)
{
  
}
  // this is the id of the gallery that will be displayed,
  //http get will have a int param

  
ngOnInit()
{
  this.getDataMock();
  this.signalRConnection(); 
  this.MyId=(Number).parseInt(this._Activatedroute.snapshot.paramMap.get("id"));
    
  console.log(this.MyId);
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
  this.unsubscribes.push(this.http.get(this.url).subscribe(data=>
    {
      this.elements=data;
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
  }

  //method that listen to signalR messages from backend and verify them
  signalRDirectiveListener(){
    //provvisory: invoke signalr method 
    this.connection
    .invoke('SendMessage', 2)
    .catch(err => console.error(err));
    //what happens whe ReceiveMessage signalR function is invoked
    this.connection.on("ReceiveMessage", (n)=>
    {
      if(n<=this.elements.length&&n>=0)
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
  

}

