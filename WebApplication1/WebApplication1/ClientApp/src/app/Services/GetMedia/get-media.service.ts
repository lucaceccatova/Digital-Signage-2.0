import { Injectable } from '@angular/core';
import { HttpClient  } from '@angular/common/http';
import { element } from '../../Models/Element';
import { map } from 'rxjs/operators';
import { r3JitTypeSourceSpan } from '@angular/compiler';
import {Observable} from 'rxjs';
const heroes = '[{"Name":"Foto1","Descritpion":"Una foto molto bella","Type":"img","Time":10,"Date":"05/06/2019","Path":"C:/"},{"Name":"Foto1","Descritpion":"Una foto molto bella","Type":"img","Time":10,"Date":"05/06/2019","Path":"C:/"}]';
@Injectable({
  providedIn: 'root'
})

export class GetMediaService {
  
  constructor(private http:HttpClient) {
  
  }
    
public get(url:string)
{
  return this.http.get<Array<element>>(url);
}



 }
      
    
