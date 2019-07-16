import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GetListService } from 'src/app/services/getListService/get-list.service';

@Component({
  selector: 'app-category-view',
  templateUrl: './category.component.view.html',
  styleUrls: ['./category.component.view.scss']
})

export class CategoryComponentView implements OnInit {
@Output() public onUploadFinished = new EventEmitter();

  list:listMedia[]=[];
  constructor(private http:GetListService) { }


  ngOnInit() {
    this.getList();
  }
  getList()
  {
    this.http.getList("/assets/Json/loadeddata.list.json").subscribe(data=>{
    this.list=data;
    });
  }

}
