import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { listMedia } from 'src/app/Model/listMedia';
import { media } from 'src/app/Model/media';

@Injectable({
  providedIn: 'root'
})
export class GetObjectService {

  
  constructor(private http:HttpClient) { }

  getList(url:string)
  {
    return this.http.get<listMedia[]>(url);
  }
  public getMedia(url:string)
  {
    return this.http.get<media[]>(url);
  }
}
