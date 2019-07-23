import { Component, OnInit, Input } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { tireShareService } from 'src/app/Services/shareTiresService/shareTireService';
import $ from 'jquery';
import { SignalRService } from 'src/app/Services/signalRService/signal-r.service';
@Component({
    selector: 'tireShow-component',
    templateUrl: './tire-show.component.html',
    styleUrls: ['./tire-show.component.scss']
})
export class tireShowComponent implements OnInit {
    constructor(private tiresStream:tireShareService ,private listner:SignalRService) { }
    @Input() tires:tire[];
    id:number;

    ngOnInit(): void { 
        this.tires=this.tiresStream.tires;
        this.mockFunction();
        $("#first").animate({opacity:1},700);
        setTimeout(() => {
            $("#second").animate({opacity:1},700);
            setTimeout(() => {
                $("#third").animate({opacity:1},700);

            }, 700);
        }, 700);
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
            this.id=this.findId(data);
        });
    }
    findId(id:number):number
    {
        return this.tires[id].id;
    }
}
