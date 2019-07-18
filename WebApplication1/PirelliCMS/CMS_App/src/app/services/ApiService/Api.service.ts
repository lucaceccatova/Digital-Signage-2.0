import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class apiService {

  
  constructor(private http:HttpClient) { }

  post(url:string, item:any)
  {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })};
    return this.http.post(url,item,httpOptions);
  }
 public get_ID(url:string,id:number)
  {
    return this.http.get(url+'/'+id);
  }
  
}
