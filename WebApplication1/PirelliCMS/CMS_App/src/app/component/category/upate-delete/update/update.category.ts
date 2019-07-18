import { Component, OnInit, Input, ViewChild, ElementRef, Directive } from '@angular/core';
import { listMedia } from 'src/app/Model/listMedia';
import { FormGroup, FormControl } from '@angular/forms';
import { apiService } from 'src/app/services/PostService/post.service';
import { UploadserviceService } from 'src/app/services/UploadService/uploadservice.service';
import { HttpEventType } from '@angular/common/http';
import { PathResponseService } from 'src/app/services/pathServie/path-response.service';
import { MatDialogRef } from '@angular/material';



@Component({
    selector: 'update-category',
    templateUrl: './update.category.html',
    styleUrls: ['./update.category.scss']
})
export class UpdateCategory implements OnInit {
    @ViewChild('fileInput',{static: false}) someInput: ElementRef;

    constructor(private upload:UploadserviceService,private api:apiService,
        private path:PathResponseService,public dialogRef: MatDialogRef<UpdateCategory>,
        ) { 
    }
    file:File;
    urlFile="https://localhost:44303/api/upload";urlObject="https://localhost:44303/api/updatecategory";
    @Input() category:listMedia;
    fileInput:string=null;
    updatedCategory:listMedia={
        id:0,
        name:null,
        description:null,
        path:null,     
    };
    form: FormGroup;
    uploading=false;uploadProgress=0;

    ngOnInit(): void {
        this.initializeCat();
        this.form=new FormGroup({
            'name':new FormControl(),
            'description':new FormControl(),
            'file':new FormControl()
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
                this.category.path=this.path.responseTranslater(data.body);
                this.assignName_Description();
                this.post_Object();
            }             
            });
       }
       else{
        this.assignName_Description();
        this.post_Object();
       }
       
    }
    initializeCat()
    {
            this.updatedCategory.id=0;
            this.updatedCategory.name=this.category.name;
            this.updatedCategory.description=this.category.description;
            this.updatedCategory.path=this.category.path;
    }
    post_Object()
    {
        this.api.post(this.urlObject,this.category).subscribe(data=>
            {
                console.log(data);
            });
            this.dialogRef.close();

    }
    assignName_Description()
    {
        if(this.form.get('name').dirty)
            this.category.name=this.form.get("name").value; 
        if(this.form.get('description').dirty)
            this.category.description=this.form.get("description").value; 
                
    }
    updateFile(file:File)
    {
        this.file=file;
        
    }
}
