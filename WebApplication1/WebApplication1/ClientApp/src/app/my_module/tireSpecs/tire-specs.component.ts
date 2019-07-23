import { Component, OnInit } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { element } from 'src/app/Models/Element';

@Component({
    selector: 'tire-spec',
    templateUrl: './tire-specs.component.html',
    styleUrls: ['./tire-specs.component.scss']
})
export class tireSpecsComponent implements OnInit {
    constructor() { }
    tire:tire;
    videos:element[]=[];
    ngOnInit(): void { 
        this.mock();
        this.mockVideo();
    }
    mockVideo()
    {
        this.videos.push({
                id:4,name:"Pirelli Girl",description:"Una bella città",format:0,timer:15,create_date:"2019-07-06T00:00:00",path:"assets/Movie/pirelliGirl.mp4",ListaID:1
            },
            {
                id:4,name:"Pirelli Girl",description:"Una bella città",format:0,timer:15,create_date:"2019-07-06T00:00:00",path:"assets/Movie/pirelliGirl.mp4",ListaID:1
            },{
                id:4,name:"Pirelli Girl",description:"Una bella città",format:0,timer:15,create_date:"2019-07-06T00:00:00",path:"assets/Movie/pirelliGirl.mp4",ListaID:1
            }
        );
    }
    mock()
    {
        this.tire={
                    id:0,
                    model:"Pzero",
                    tirePath:"/assets/img/tireShow.jpg",
                    tireType:"Pista",
                    price:200,
                    size:21
        }
    }
}
