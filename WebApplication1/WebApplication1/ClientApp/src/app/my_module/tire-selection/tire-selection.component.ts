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
   //to mockup
    this.http.get<car>(this.url).subscribe(data=>
      {
        this.car=data;
        this.selectedTire=this.car.tires[0];
        this.connectionService.connection.on("sendTire",data=>
        {
          this.pitStop(data);
        })
      });

      
      //todb
     // this.car=this.UnivShare.sharedObject;
  }
  pitStop(i:tire)
  {
    this.selectedTire=i;
  }

}
