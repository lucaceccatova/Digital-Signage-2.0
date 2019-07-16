import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import { categoryDialog } from '../are-you-sure-about-it/are-you-sure-about-it.component';
import { UploadComponent } from '../../upload/mediaAdd/upload.component';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.add.html',
  styleUrls: ['./category.component.add.scss']
})
export class CategoryComponent implements OnInit {
@Output() public onUploadFinished = new EventEmitter();

url="https://localhost:44303/api/getcategories";
  public category:listMedia={
    id:0,
    name:null,
    description:null,
    path:"",

  };
  formController:FormGroup;
  uploading:Boolean=false;uploadProgress=0;
  file:File;filename;

  constructor(private dialog:MatDialog,private http:UploadserviceService) { }


  ngOnInit() {
    this.formController=new FormGroup({
      'name':new FormControl(this.category.name, Validators.required),
      'description':new FormControl(this.category.description,Validators.required),
      'fileName':new FormControl( this.filename, [Validators.required])
    });

  }
  onFileChange(event:any){
    console.log(event);
    this.file = event.target.files;
    var reader = new FileReader();
    
    reader.readAsDataURL(this.file[0]); 
    reader.onload = (_event) => { 
      this.filename = reader.result; 
    }
  }
  sendCategory()
  {
   this.assignFormValue(); 
    const confDialog=this.dialog.open(categoryDialog,{
        data:{
          category:this.category,
          file:this.filename
        }
    });
    confDialog.afterClosed().subscribe(data=>
      {
        if(data==true)
        {
          
        }
      });
  }
//upload to DB
upload()
{
  this.http.uploadFile(this.url,this.file).subscribe(data=>
    {
      console.log(data);
    });
}
  assignFormValue()
  {
    this.category.name=this.formController.controls['name'].value;
    this.category.description=this.formController.controls['description'].value;
  }

  
}
