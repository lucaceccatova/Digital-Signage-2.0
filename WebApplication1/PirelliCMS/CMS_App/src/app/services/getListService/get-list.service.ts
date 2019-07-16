import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { listMedia } from 'src/app/Model/listMedia';

@Injectable({
  providedIn: 'root'
})
export class GetListService {

  
  constructor(private http:HttpClient) { }

  getList(url:string)
  {
    return this.http.get<listMedia[]>(url);
  }
}
