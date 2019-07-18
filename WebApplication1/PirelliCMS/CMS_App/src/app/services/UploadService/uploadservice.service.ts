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

  uploadFile(url: string, file: Blob){
    var fileToUpload=<File>file[0];
    let formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.http.post(url, formData,{reportProgress: true, observe: 'events'});
  }
}
