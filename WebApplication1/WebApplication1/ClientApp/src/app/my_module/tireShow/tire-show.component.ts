import { Component, OnInit, Input } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { tireShareService } from 'src/app/Services/shareTiresService/shareTireService';
import $ from 'jquery';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
import { element } from '../../Models/Element';
import { RouterModule, Router } from '@angular/router';
@Component({
    selector: 'tireShow-component',
    templateUrl: './tire-show.component.html',
    styleUrls: ['./tire-show.component.scss']
})
export class tireShowComponent implements OnInit {
  constructor(private tiresStream: tireShareService, private listner: SignalRService,
  private router: Router) { }
    @Input() tires:tire[];
  id: number;
  videos: element[] = [];
  selectedTire: tire;

    ngOnInit(): void { 
        this.tires=this.tiresStream.tires;
        console.log(this.tires);
       // this.mockFunction();
        $("#first").animate({opacity:1},700);
        setTimeout(() => {
            $("#second").animate({opacity:1},700);
            setTimeout(() => {
                $("#third").animate({opacity:1},700);

            }, 700);
        }, 700);
        this.signalRListner();
        }
    mockFunction()
    {
        this.tires.push({id:0,model:"Pzero",
                    tirePath:"/assets/img/tireShow.jpg",
                    tireType:"Pista",
                    price:200,
                    size:21},   
                    {id:0,model:"Pzero",
                    tirePath:"/assets/img/tireShow.jpg",
                    tireType:"Pista",
                    price:200,
                    size:21},
                    {id:0,model:"Pzero",
                    tirePath:"/assets/img/tireShow.jpg",
                    tireType:"Pista",
                    price:200,
                    size:21});
    }
    signalRListner()
    {
      this.listner.connection.on("receiveAskIdTire",data=>
        {
          console.log(data);
          this.selectedTire = this.tires[data-1];
          this.id = this.findId(data);
          this.listner.connection.send("SendIdTire",this.id)
          .catch(err => console.error(err));
        });
      this.listner.connection.on("receiveTireVideos", data => {
        this.videos = data;
        console.log(data);
      this.goToSpec();
      });
      this.listner.connection.on("tireShow",data=>
      {
        console.log(this.tires);
        this.tires=data;
        console.log(this.tires);

      });

  }
  goToSpec() {
    this.tiresStream.selectedTire = this.selectedTire;
    this.tiresStream.videos = this.videos;
    this.router.navigateByUrl("/specs");
  }
    findId(id:number):number
    {
        return this.tires[id].id;
    }
}
