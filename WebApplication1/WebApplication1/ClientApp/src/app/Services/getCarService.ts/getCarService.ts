import { Injectable } from '@angular/core';
import { HttpClient  } from '@angular/common/http';
import {car} from '../../Models/carModel';
@Injectable()
export class getCarService {
    constructor(private http:HttpClient)
    {

    }
getCar()
    {
        return this.http.get<car>("path").subscribe();
    }
}