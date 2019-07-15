import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest, HttpHeaders } from '@angular/common/http';
import { media } from '../../Model/media';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadserviceService {

  formdata;
  constructor(private http:HttpClient) { }
upload(media:File,url:string)
{
  console.log(media);
 this.formdata=new FormData();
 this.formdata.append('UploadedFile', media);
  this.http.post(url,media).subscribe(res=>
    {
      console.log(res);
    });
}
uploadFile(url: string, file: Blob){

  var fileToUpload=<File>file[0];
  let formData = new FormData();
  formData.append('file', fileToUpload, fileToUpload.name);
      return this.http.post(url, formData,{reportProgress: true, observe: 'events'});

}
}
