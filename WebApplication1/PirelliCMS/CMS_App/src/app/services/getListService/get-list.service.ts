import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { listMedia } from 'src/app/Model/listMedia';

@Injectable({
  providedIn: 'root'
})
export class GetListService {

  url:string="/assets/Json/loadeddata.list.json";
  constructor(private http:HttpClient) { }

  getList()
  {
    return this.http.get<listMedia[]>(this.url);
  }
}
