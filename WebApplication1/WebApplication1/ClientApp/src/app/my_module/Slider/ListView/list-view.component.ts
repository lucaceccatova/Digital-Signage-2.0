import { Component, OnInit } from '@angular/core';
import { listMedia } from '../../../Models/listMedia';
import { ListService } from '../../../Services/ListService/list-service.service';
import {ActivatedRoute} from '@angular/router';
@Component({
  selector: 'app-list-view',
  templateUrl: './list-view.component.html',
  styleUrls: ['./list-view.component.scss']
})
export class ListViewComponent implements OnInit {
  MyList :listMedia[];
  url:string="https://localhost:44303/api/test/getlista";
  constructor(private list:ListService) { }

  ngOnInit() {
    this.list.get(this.url).subscribe(medialist=>
    {
      this.MyList=medialist;
    });
  }

}
