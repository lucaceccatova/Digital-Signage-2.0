import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { car } from '../../Models/carModel';
import { ShareService } from 'src/app/Services/UniversalShare/universalShareService';
import { tire } from 'src/app/Models/tire';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';

@Component({
  selector: 'app-tire-selection',
  templateUrl: './tire-selection.component.html',
  styleUrls: ['./tire-selection.component.scss']
})
export class TireSelectionComponent implements OnInit {
  car:car;
  selectedTire:tire;

  url:string='assets/loadeddata.tire.json';
  constructor(private http:HttpClient,private UnivShare:ShareService,
    private connectionService:SignalRService) { }

  ngOnInit() {
   //to mockup get data from json 
    this.http.get<car>(this.url).subscribe(data=>
      {
        this.car=data;
        this.selectedTire=this.car.tires[0];
      }); 

      
      //todb get data from service that has been initialized by previous component
   /*   
    this.car = this.UnivShare.sharedObject;
    this.selectedTire = this.car.tires[0];
    console.log(this.car);
    this.signalR();
    */
  }

  signalR()
  {
    this.connectionService.connection.on("reciveTire", data => {
      this.pitStop(data);
    //to disable access to show video
    this.connectionService.disconnect("showVideo");
    });
  }
  

  //change img for tires
  pitStop(i:tire)
  {
    this.selectedTire=i;
  }

}
