import { Injectable } from '@angular/core';
import { element } from 'src/app/Models/Element';

@Injectable(
    {
        providedIn:'root',
    }
)
export class shareElementsService {
elements:element[]=[];
connection;
}