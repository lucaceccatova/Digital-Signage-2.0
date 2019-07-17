import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  
  constructor(private http:HttpClient) { }

  post(url:string, item:any)
  {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })};
    return this.http.post(url,item,httpOptions);
  }
}
