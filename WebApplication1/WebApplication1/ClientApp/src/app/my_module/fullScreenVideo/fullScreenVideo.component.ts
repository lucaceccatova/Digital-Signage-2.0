import { Component, Input } from "@angular/core";
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';
import { element } from 'src/app/Models/Element';
import {Location} from '@angular/common';
import { TimeoutIdService } from 'src/app/Services/sharedServices/timeout-id.service';
@Component({
    templateUrl:'fullScreenVideo.component.html'
})

export class fullScreenVideo
{
    singleVideo:element;
    constructor(private stream:sharedStringService,private location:Location,
        private timeout:TimeoutIdService)  { }

    ngOnInit(): void {
    
        this.singleVideo=this.stream.singleVideo;
        this.timeout.videoTimeout=setTimeout(() => {
           this.location.back(); 
        }, this.singleVideo.timer*1000);
    }
}