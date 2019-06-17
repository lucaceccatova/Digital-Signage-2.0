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
  //
  private connection : HubConnection;
  public directves:number[]=[];

constructor(private http:GetMediaService,private dir:ServerListnerService)
{
  
}
@Input()
listID:number;
ngOnInit()
{
  this.getDataMock();
  this.signalRConnection();  
  
 
}
ngOnDestroy()
{
  this.unsubscribes.forEach(element => {
    element.unsubscribe();
  });
}

 getDataMock()
 {
   
  this.unsubscribes.push(this.http.get(this.url).subscribe(data=>
    {
      this.elements=data;

    })  
    );
 }

slideEngine()
  {
    this.setTimeoutInterceptor=setTimeout(() => {
      if(this.dir.directves.length>this.indexInterrup)
      {
        this.startingSlide=this.dir.directves[this.dir.directves.length-1]
        this.newDirective=false;
        this.indexInterrup=this.dir.directves.length;

      }
      else if(this.elements.length>this.startingSlide+1)
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

  signalRConnection(){
  this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
  .configureLogging(signalR.LogLevel.Information)
  .build();
    this.connection
    .start()
    .then(()=>console.log("connection started"))
    .catch(err=>console.log("Errore di connessione"));
  }
  signalRDirectiveListener(){
    this.connection
    .invoke('SendMessage', 2)
    .catch(err => console.error(err));
  

    this.connection.on("ReceiveMessage", (n)=>
    {
      this.startingSlide=n;
      clearTimeout(this.setTimeoutInterceptor);
      this.slideEngine();
    });

  }
}

