import { Component, OnInit, Input } from '@angular/core';



@Component({
    selector: 'delete',
    templateUrl: './delete.component.html',
    styleUrls: ['./delete.component.scss','../../../../app.component.scss']
})
export class DeleteComponent implements OnInit {

    
    constructor() { }
    @Input()message:string;
    ngOnInit(): void { }
}
