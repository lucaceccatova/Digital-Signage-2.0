import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TireSelectionComponent } from './tire-selection.component';
import { tireRouterModule } from './tireRouter';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
    declarations: [TireSelectionComponent],
    imports: [ CommonModule,tireRouterModule,HttpClientModule ],
    exports: [],
    providers: [],
})
export class tireModule {}