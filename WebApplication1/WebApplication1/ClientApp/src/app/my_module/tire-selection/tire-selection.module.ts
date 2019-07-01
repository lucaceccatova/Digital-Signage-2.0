import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TireSelectionComponent } from './tire-selection.component';
import { tireRouterModule } from './tireRouter';

@NgModule({
    declarations: [TireSelectionComponent],
    imports: [ CommonModule,tireRouterModule ],
    exports: [],
    providers: [],
})
export class tireModule {}