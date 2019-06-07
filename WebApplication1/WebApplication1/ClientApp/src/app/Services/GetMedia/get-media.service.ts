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
 elements:element[]=[];
  
  constructor(private http:HttpClient) {
  
   
  }
      GetJSON(){
          return this.http.get("https://localhost:44303/api/test/getdati")
          .pipe(
            map(Response=> Response['element']));
      }
        



      (term: string): Observable<element[]> {
        let apiURL = `${this.apiRoot}?term=${term}&media=music&limit=20&callback=JSONP_CALLBACK`;
        return this.http.get(apiURL) (1)
            .map(res => { (2)
              return res.json().results.map(item => { (3)
                return new SearchItem( (4)
                    item.trackName,
                    item.artistName,
                    item.trackViewUrl,
                    item.artworkUrl30,
                    item.artistId
                );
              });
            });
      }
      
    }
