import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PathResponseService {
  public response: {dbPath: ''};

  constructor() { }

 responseTranslater = (event) => {
    return this.response = event;
  }
}
