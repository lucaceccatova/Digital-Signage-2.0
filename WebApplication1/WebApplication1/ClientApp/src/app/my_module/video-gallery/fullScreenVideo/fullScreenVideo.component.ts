import { Component, Input } from "@angular/core";
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';
import { element } from 'src/app/Models/Element';
@Component({
    templateUrl:'fullScreenVideo.component.html'
})

export class fullScreenVideo
{
    singleVideo:element;
    constructor(private stream:sharedStringService,private router:Router)  { }

    ngOnInit(): void {
    
        this.singleVideo=this.stream.singleVideo;
        setTimeout(() => {
           this.router.navigateByUrl("/video"); 
        }, this.singleVideo.timer*1000);
    }
}