import { Component, OnInit, Input } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { tireShareService } from 'src/app/Services/sharedServices/shareTireService';
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
//the revious component must use a service to pass data trough this component
  tires:tire[];
  id: number;
  videos: element[] = [];
  selectedTire: tire;
  emptyTire:tire={
    id:-1,
    model:null,
    price:null,
    size:null,
    tirePath:null,
    tireType:null,
  }

    ngOnInit(): void { 
        this.tires=this.tiresStream.tires;
        console.log(this.tires);
       // this.mockFunction();
        $("#1").animate({opacity:1},700);
        setTimeout(() => {
            $("#2").animate({opacity:1},700);
            setTimeout(() => {
                $("#3").animate({opacity:1},700);

            }, 700);
        }, 700);
        this.signalRListner();        }
        ngOnDestroy(): void {
          //Called once, before the instance is destroyed.
          //Add 'implements OnDestroy' to the class.
          this.listner.connection.off("tireShow");
          this.listner.connection.on("tireShow",data=>
          {
            this.tiresStream.tires=data;
        this.router.navigateByUrl('/tire');
          });
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
          this.selectedTire = this.tires[data-1];
          this.id = this.findId(data);
          this.listner.connection.send("SendIdTire",this.id)
          .catch(err => console.error(err));
        });
      this.listner.connection.on("receiveTireVideos", data => {
       this.videos=data;
      this.goToSpec();
      });
      this.listner.connection.on("tireShow",data=>
      {
        console.log(data);
        this.changeTire(data);
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
    changeTire(newTires:tire[])
    {
      while(newTires.length<this.tires.length)
      {
        newTires.push(this.emptyTire);
      }
      
      newTires.forEach((tire,index) => {
        if(this.tires[index].id!=tire.id)
        {
          $("#"+(index+1)).fadeOut(700);
            setTimeout(() => {
              if(tire!=this.emptyTire)
            {
              this.tires[index]=tire;
              $("#"+(index+1)).fadeIn(700);
            }
            }, 700);
            
        }
      });
    }
}
