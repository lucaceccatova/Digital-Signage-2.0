
import { Injectable } from '@angular/core';
import { tire } from 'src/app/Models/tire';
import { element } from 'src/app/Models/Element';

@Injectable(
    {
        providedIn:'root',
    }
)
export class tireShareService {
tires:tire[];
videos:element[]=[];
selectedTire:tire;
}