
import { Injectable } from '@angular/core';
import { tire } from 'src/app/Models/tire';

@Injectable(
    {
        providedIn:'root',
    }
)
export class tireShareService {
tires:tire[]=[];
}