import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import { categoryDialog } from '../are-you-sure-about-it/are-you-sure-about-it.component';
import { UploadComponent } from '../../upload/mediaAdd/upload.component';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';
import { HttpEventType } from '@angular/common/http';
import { PathResponseService } from 'src/app/services/pathServie/path-response.service';
import { apiService } from 'src/app/services/PostService/post.service';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.add.html',
  styleUrls: ['./category.component.add.scss','../../../app.component.scss']
})
export class CategoryComponent implements OnInit {
@Output() public onUploadFinished = new EventEmitter();
urlFile="https://localhost:44303/api/upload";urlCategory="https://localhost:44303/api/addcategory";
  public category:listMedia={
    id:0,
    name:null,
    description:null,
    path:"",

  };
  formController:FormGroup;
  uploading:Boolean=false;uploadProgress=0;
  file:File;filename;
  constructor(private dialog:MatDialog,private http:UploadserviceService,
    private path:PathResponseService, private post:apiService) { }


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
          this.upload();
        }
      });
  }
//upload to DB
upload()
{
  this.http.uploadFile(this.urlFile,this.file).subscribe(data=>
    {
      if (data.type === HttpEventType.UploadProgress)
        {
          this.uploading=true;
          this.uploadProgress=(data.loaded*100/data.total);
          console.log(data);
        }
        else if (data.type === HttpEventType.Response) 
         {
           this.category.path=this.path.responseTranslater(data.body);
           
           //to reset form for a new insert
           setTimeout(() => {
            this.uploading=false;
            this.formController.reset();
            Object.keys(this.formController.controls).forEach(key => {
              this.formController.get(key).setErrors(null) ;
            });           }, 1000);
         this.postCategory();
         }  
    });
}

  assignFormValue()
  {
    this.category.name=this.formController.controls['name'].value;
    this.category.description=this.formController.controls['description'].value;
  }

  postCategory()
  {
    console.log(this.category);
    this.post.post(this.urlCategory,this.category).subscribe(data=>
      {
        
      });
  }
  
}
