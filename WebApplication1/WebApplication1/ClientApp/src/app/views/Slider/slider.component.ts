import { Component, OnInit ,OnDestroy, Output} from '@angular/core';

import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import {GetMediaService}from '../../Services/GetMedia/get-media.service';
import {element} from '../../Models/Element';
import { observable, Subscription, from, Observable, config } from 'rxjs';
import { ViewEncapsulation } from '@angular/core'
import {NgbCarouselConfig} from '@ng-bootstrap/ng-bootstrap';

import { map } from 'rxjs/operators';
import { getLocaleDateFormat } from '@angular/common';
import {CommonModule} from "@angular/common";
import { compileBaseDefFromMetadata } from '@angular/compiler';
import {CarouselsComponent} from '../../views/base/carousels.component';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
@Component({
  encapsulation:ViewEncapsulation.None,
  templateUrl: 'slider.component.html',
  styleUrls:['./slider.scss']
})
export class SliderComponent implements OnInit,OnDestroy
{
  //url:string="/assets/loadeddata.json"
  url:string="https://localhost:44303/api/test/getdati"
  public elements:element[];
  unsubscribes: Subscription[]=[];
  nick='prova';
  message='';
  messageString:string[]=[];
  private connection : HubConnection;
  startingSlide=1;

constructor(private http:GetMediaService)
{
  
}
ngOnInit()
{
  this.getDataMock();
 
  
  //  this.slideEngine(this.elements);
 
}
public SendMessage():void
{
  this.connection.invoke("sendToAll",this.nick,this.message)
  .catch();
  this.message='';
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
 Send()
 {
   this.messageString.push(this.message);
   this.message='';
 }
 DebugConnection()
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

  this.connection.on("sendToAll",(nick :string,message :string)=>
  {
  const text=nick+'#'+message;
  });
  
 }
slideEngine()
  {
    setTimeout(() => {
      this.startingSlide++;
      this.slideEngine();
      console.log(this.elements);
    }, this.elements[this.startingSlide].timer*1000);
  }
}

