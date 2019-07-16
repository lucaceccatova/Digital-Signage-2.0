import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { media } from 'src/app/Model/media';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';
import { HttpEventType } from '@angular/common/http';
import { FormGroup,FormBuilder,
  Validators,
  FormControl } from '@angular/forms';
import { GetListService } from 'src/app/services/getListService/get-list.service';
import { listMedia } from 'src/app/Model/listMedia';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
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
   mediaType:null,
   path:null

 };
 //file to upload
 file:File;
//
list:listMedia[]=[];
 public response: {dbPath: ''};
 form: FormGroup;
uploadProgress:number=0; uploading=false;
  constructor(private http:UploadserviceService,private formBuilder: FormBuilder, private stremList:GetListService) { }
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
public uploadFinished = (event) => {
  this.response = event;
  this.media.path=this.response.dbPath;

            console.log(this.media);         
}
send() {
  this.media=this.form.value;
  this.timeConverter();
  
  this.http.uploadFile("https://localhost:44303/api/upload",this.file).subscribe(data=>
  {
    if (data.type === HttpEventType.UploadProgress)
        {
          this.uploading=true;
          this.uploadProgress=(data.loaded*100/data.total);
          console.log(data);
        }
        else if (data.type === HttpEventType.Response) 
         {
           console.log(data);
          this.uploadFinished(data.body); 
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
}