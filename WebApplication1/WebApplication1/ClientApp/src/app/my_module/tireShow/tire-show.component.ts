import { Component, OnInit, Input } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { tireShareService } from 'src/app/Services/sharedServices/shareTireService';
import $ from 'jquery';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
import { element } from '../../Models/Element';
import { RouterModule, Router } from '@angular/router';
enum anim {enterSandMan, exitLight, theNumber}
{

}
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
  visible:anim[]=[
    anim.theNumber,
    anim.theNumber,
    anim.theNumber
  ];
rotation=[false,false,false];
    ngOnInit(): void { 
        this.tires=this.tiresStream.tires;
        console.log(this.tires);
      this.visible[0]=anim.enterSandMan;
      this.setAnim(anim.enterSandMan);

        this.signalRListner();        
      }
        
        
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
      this.setAnim(anim.exitLight);
      setTimeout(() => {
        this.changeTireElement(newTires);  
      }, 1000);
    }
    changeTireElement(newTires:tire[])
    {
      while(newTires.length<this.tires.length)
      {
        newTires.push(this.emptyTire);
      }
      console.log(this.tires);
      newTires.forEach((tire,index) => {
      //  if(tire!=this.emptyTire)
        {
          if(tire!=this.emptyTire)
              this.tires[index]=tire;
          else
            this.tires[index]=null;
        }
      });

      setTimeout(() => {
        this.setAnim(anim.enterSandMan);
      }, 1000);
           
    }



    incoming(id:number)
    {
      if(this.visible[id-1]==anim.enterSandMan)
        return true;
      return false;
    }
    outgoing(id:number)
    {
      if(this.visible[id-1]==anim.exitLight)
      {
        return true;
      }
      return false;
    }
    
    setAnim(animation:anim)
    {
      setTimeout(() => {
        this.visible[0]=animation;
        if(animation==anim.enterSandMan)
        this.rotation[0]=true;
        else
        this.rotation[0]=false;
        setTimeout(() => {
          this.visible[1]=animation;
          if(animation==anim.enterSandMan)
          this.rotation[1]=true;
          else
          this.rotation[1]=false;
          setTimeout(() => {
            this.visible[2]=animation;
            if(animation==anim.enterSandMan)
            this.rotation[2]=true;
            else
            this.rotation[2]=false;
          }, 1000);
        }, 1000);   
      }, 1000);
      
    }
    rotating(id:number)
    {
      if (this.rotation[id]==true)
      {
        return true;
      }
      return false;
    }

}
