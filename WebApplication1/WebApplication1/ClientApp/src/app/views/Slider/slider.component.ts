import { Component, OnInit ,OnDestroy, Output} from '@angular/core';

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
@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent implements OnInit,OnDestroy
{
  url:string="/assets/loadeddata.json"
  //url:string="https://localhost:44303/api/test/getdati"
  public elements:element[];
  unsubscribes: Subscription[]=[];
  //startingSlide is the index of the media displayed in slider
  startingSlide=0;
  //id, server directive for showing a specific slide 
  id:number;
  newDirective:boolean;
  //observable for newDirective
  public interrupt:Observable<boolean>;


constructor(private http:GetMediaService,private dir:ServerListnerService)
{
  
}
ngOnInit()
{
  this.getDataMock();
 
  
  this.sliderListner();
 
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
 
 /*DebugConnection()
 {
   console.log("Ciao");
  //this.connection= new HubConnectionBuilder().withUrl('http://localhost:4200/chat').build();
this.connection=new HubConnectionBuilder().withUrl('https://localhost:44303/voice')
.configureLogging(signalR.LogLevel.Information)
.build();
  this.connection
  .start()
  .then(()=>console.log("connection started"))
  .catch(err=>console.log("Errore di connessione"));


  //

  this.connection.on("sendId",(numero:number)=>
  {
    this.id=numero;
    this.newDirective=true;

  });
  
 }*/
slideEngine()
  {
    setTimeout(() => {
      if(this.newDirective==true)
      { this.id=null;
        this.startingSlide=this.id;
        this.newDirective=false;
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
  sliderListner()
  {
   this.dir.getDirective().subscribe(data=>
    {
      
      this.id==data;
      if(this.id<this.elements.length && this.id>=0)
      {
      this.newDirective==true;
      }
    });
  }
}

