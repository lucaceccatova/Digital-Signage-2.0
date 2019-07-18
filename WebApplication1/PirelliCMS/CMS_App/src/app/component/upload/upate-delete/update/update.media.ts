import { Component, OnInit, Input } from '@angular/core';
import { media } from 'src/app/Model/media';
import { FormGroup, FormControl } from '@angular/forms';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';
import { HttpEventType } from '@angular/common/http';
import { PathResponseService } from 'src/app/services/pathServie/path-response.service';
import { apiService } from 'src/app/services/ApiService/Api.service';
import { MatDialogRef } from '@angular/material';
import { listMedia } from 'src/app/Model/listMedia';
import { GetObjectService } from 'src/app/services/getListService/get-object.service';

@Component({
    selector: 'update-media',
    templateUrl: './update.media.html',
    styleUrls: ['./update.media.scss']
})
export class media_mod implements OnInit {
    constructor(private upload:UploadserviceService,
        private path:PathResponseService,
        private api:apiService,
        public dialogRef:MatDialogRef<media_mod>,
        private getObject:GetObjectService) { }
    form:FormGroup;
    file:File;urlFile="https://localhost:44303/api/upload";
    urlMedia="https://localhost:44303/api/updatemedia";
    urlList="https://localhost:44303/api/getcategories"
    uploading=false;uploadProgress=0;
    listCategory:listMedia[]=[];
    @Input() media:media;
    ngOnInit(): void {
        this.form=new FormGroup({
        'name':new FormControl(this.media.name),
        'description':new FormControl(this.media.description),
        'file':new FormControl(),
        'listId':new FormControl(),
        'timer':new FormControl(this.media.timer)
        });


        this.getObject.getList(this.urlList).subscribe(data=>
            {
                this.listCategory=data;
            })
     }

     update()
     {
        if(this.form.get('file').dirty)
        {
            this.upload.uploadFile(this.urlFile,this.file).subscribe(data=>
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
                 this.assignProperties();
                 this.post_Object();
                }             
             });
        }
        else{
         this.assignProperties();
         this.post_Object();
            }
        console.log(this.media);
     }

     post_Object()
     {
        this.api.post(this.urlMedia,this.media).subscribe(data=>
            {
                this.dialogRef.close();
            });

     }
     assignProperties()
     {
        if(this.form.get('name').dirty)
        this.media.name=this.form.get("name").value; 
    if(this.form.get('description').dirty)
        this.media.description=this.form.get("description").value; 
    if(this.form.get('mediaType').dirty)
        this.media.listId=this.form.get("listId").value; 
    if(this.form.get('timer').dirty)
        this.media.timer=this.form.get("timer").value; 
            
    }
     updateFile(file:File)
    {
        this.file=file;
        
    }
}
