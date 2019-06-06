import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from "rxjs/operators";
import { element } from '../../Models/Element';

const heroes = '[{"Name":"Foto1","Descritpion":"Una foto molto bella","Type":"img","Time":10,"Date":"05/06/2019","Path":"C:/"},{"Name":"Foto1","Descritpion":"Una foto molto bella","Type":"img","Time":10,"Date":"05/06/2019","Path":"C:/"}]';
@Injectable({
  providedIn: 'root'
})

export class GetMediaService {
 elements:element[];
  
  constructor(private http:HttpClient,private res :HttpHeaders) { 
   
  }
      GetJSON():element[]{
         this.http.get("http://localhost44303/api/test/getdati").pipe(
         map((response: Response)=>
         {
           this.elements=JSON.parse(response.text.toString());
         }))
         return this.elements;
        }
        

      
}
