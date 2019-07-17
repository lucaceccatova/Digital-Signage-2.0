import { Component, OnInit, Input } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';

@Component({
    selector: 'update-category',
    templateUrl: './update.category.html',
    styleUrls: ['./update.category.scss']
})
export class UpdateCategory implements OnInit {
    constructor() { }

    @Input() category:listMedia;
    ngOnInit(): void { }
}
