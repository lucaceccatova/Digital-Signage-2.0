import { Component, Input } from "@angular/core";
import { sharedStringService } from 'src/app/Services/sharedServices/sharedString.service';
import { Router } from '@angular/router';
@Component({
    templateUrl:'fullScreenVideo.component.html'
})

export class fullScreenVideo
{
    path;
    constructor(private stream:sharedStringService,private router:Router)  { }

    ngOnInit(): void {
        this.path=this.stream.data;
        setTimeout(() => {
           this.router.navigateByUrl("/video"); 
        }, this.stream.time*1000);

    }
}