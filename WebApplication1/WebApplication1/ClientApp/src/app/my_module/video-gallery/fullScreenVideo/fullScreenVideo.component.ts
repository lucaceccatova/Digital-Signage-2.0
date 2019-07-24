import { Component, Input } from "@angular/core";
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';
import { element } from 'src/app/Models/Element';
import {Location} from '@angular/common';
@Component({
    templateUrl:'fullScreenVideo.component.html'
})

export class fullScreenVideo
{
    singleVideo:element;
    constructor(private stream:sharedStringService,private location:Location)  { }

    ngOnInit(): void {
    
        this.singleVideo=this.stream.singleVideo;
        setTimeout(() => {
           this.location.back(); 
        }, this.singleVideo.timer*1000);
    }
}