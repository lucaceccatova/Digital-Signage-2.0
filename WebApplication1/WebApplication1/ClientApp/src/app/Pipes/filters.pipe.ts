import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'upperCase'
})
export class FiltersPipe implements PipeTransform {

  transform(low: string): string {
    return low.toUpperCase();
  }

}
