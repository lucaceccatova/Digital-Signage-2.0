import * as tslib_1 from "tslib";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { myRouter } from './router/router.module';
import { FiltersPipe } from './Pipes/filters.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
let AppModule = class AppModule {
};
AppModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            AppComponent,
            FiltersPipe,
        ],
        imports: [
            BrowserModule, BrowserAnimationsModule,
            myRouter,
        ],
        exports: [],
        providers: [],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map