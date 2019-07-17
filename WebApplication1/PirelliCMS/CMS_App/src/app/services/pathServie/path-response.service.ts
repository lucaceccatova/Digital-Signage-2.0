import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PathResponseService {
  public response: {dbPath: ''};

  constructor() { }

 responseTranslater = (event) => {
     this.response = event;
     return this.response.dbPath;
  }
}
