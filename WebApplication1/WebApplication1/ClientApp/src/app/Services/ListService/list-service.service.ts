import { Injectable } from '@angular/core';
import { HttpClient, HttpDownloadProgressEvent  } from '@angular/common/http';
import {listMedia} from '../../Models/listMedia';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  constructor(private http:HttpClient) { 
  }
  public get(url:string){
    return this.http.get<Array<listMedia>>(url);
  }
}
