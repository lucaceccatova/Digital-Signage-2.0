import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})

export class CategoryComponent implements OnInit {
@Output() public onUploadFinished = new EventEmitter();

  category:listMedia={
    id:null,
    name:"Name",
    description:"Description",
    path:null,

  };
  formController:FormGroup;
  uploading:Boolean=false;uploadProgress=0;
  constructor() { }


  ngOnInit() {
    this.formController=new FormGroup({
      'Categoryname':new FormControl(this.category.name,Validators.required),
      'CategoryDescription':new FormControl(this.category.description,Validators.required)
    });
  }
  onFileChange(e:Event)
  {
    
  }

}
