import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { media } from 'src/app/Model/media';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';
import { HttpEventType } from '@angular/common/http';
import { FormGroup,FormBuilder,
  Validators,
  FormControl } from '@angular/forms';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';
import { listMedia } from 'src/app/Model/listMedia';
import { PathResponseService } from 'src/app/services/pathServie/path-response.service';
import { apiService } from 'src/app/services/ApiService/Api.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss','../../../app.component.scss']
})

export class UploadComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter();

  //to identify seconds, minuts or hours
  timeMoltiplier:number=1;
 public media:media={
   name:null,
   description:null,
   timer:undefined,
   format:null,
   listId:null,
   path:null

 };
 //file to upload
 file:File;
//
list:listMedia[]=[];
 form: FormGroup;
uploadProgress:number=0; uploading=false;
//string api
urlFile="https://localhost:44303/api/upload";urlPost="https://localhost:44303/api/addmedia";
//
  constructor(private http:UploadserviceService,private formBuilder: FormBuilder, private stremList:GetObjectService,
    private path:PathResponseService,private post:apiService) { }
  fileName = '';
  ngOnInit() {  
    this.getList();
    this.form=new FormGroup({
    'name': new FormControl(this.media.name, [
      Validators.required,
      Validators.minLength(4),]),
    'description': new FormControl(this.media.description,[Validators.required]),
    'timer': new FormControl(this.media.timer, [Validators.required]),
    'format':new FormControl(this.media.format,Validators.required),
    'listId':new FormControl(this.media.listId,Validators.required),
    'mediaFile':new FormControl( this.fileName, [Validators.required])
  });

}
send() {
  this.media=this.form.value;
  this.timeConverter();
  
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
          this.media.path=this.path.responseTranslater(data.body);
          this.postMedia();
         }  
  })
}
  onFileChange(files:File){
    this.file = files;
  }
  timeConverter()
  {
    this.media.timer=this.media.timer*this.timeMoltiplier;

  }
  getList()
  {
    this.stremList.getList("/assets/Json/loadeddata.list.json").subscribe(data=>{
      this.list=data;
    });
  }
  postMedia()
  {
    
    this.post.post(this.urlPost,this.media).subscribe(data=>
      {
        console.log(data);
      });
  }
}