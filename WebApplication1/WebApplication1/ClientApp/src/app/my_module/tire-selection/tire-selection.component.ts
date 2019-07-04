import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { car } from '../../Models/carModel';
import { ShareService } from 'src/app/Services/UniversalShare/universalShareService';

@Component({
  selector: 'app-tire-selection',
  templateUrl: './tire-selection.component.html',
  styleUrls: ['./tire-selection.component.scss']
})
export class TireSelectionComponent implements OnInit {
  car:car;
  url:string='assets/loadeddata.tire.json';
  constructor(private http:HttpClient,private UnivShare:ShareService) { }

  ngOnInit() {
   //to mockup
    //this.http.get<car>(this.url).subscribe(data=>
    //  {
    //    this.car=data;
        
    //  })

      //todb
      this.car=this.UnivShare.sharedObject;
      console.log(this.car);
  }

}
