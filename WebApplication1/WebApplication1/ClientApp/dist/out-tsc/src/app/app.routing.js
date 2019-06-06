import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
// Import Containers
import { DefaultLayoutComponent } from './containers';
import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
export var routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
    },
    {
        path: '404',
        component: P404Component,
        data: {
            title: 'Page 404'
        }
    },
    {
        path: '500',
        component: P500Component,
        data: {
            title: 'Page 500'
        }
    },
    {
        path: 'login',
        component: LoginComponent,
        data: {
            title: 'Login Page'
        }
    },
    {
        path: 'register',
        component: RegisterComponent,
        data: {
            title: 'Register Page'
        }
    },
    {
        path: '',
        component: DefaultLayoutComponent,
        data: {
            title: 'Home'
        },
        children: [
            {
                path: 'base',
                loadChildren: function () { return import('./views/base/base.module').then(function (m) { return m.BaseModule; }); }
            },
            {
                path: 'buttons',
                loadChildren: function () { return import('./views/buttons/buttons.module').then(function (m) { return m.ButtonsModule; }); }
            },
            {
                path: 'charts',
                loadChildren: function () { return import('./views/chartjs/chartjs.module').then(function (m) { return m.ChartJSModule; }); }
            },
            {
                path: 'dashboard',
                loadChildren: function () { return import('./views/dashboard/dashboard.module').then(function (m) { return m.DashboardModule; }); }
            },
            {
                path: 'icons',
                loadChildren: function () { return import('./views/icons/icons.module').then(function (m) { return m.IconsModule; }); }
            },
            {
                path: 'notifications',
                loadChildren: function () { return import('./views/notifications/notifications.module').then(function (m) { return m.NotificationsModule; }); }
            },
            {
                path: 'theme',
                loadChildren: function () { return import('./views/theme/theme.module').then(function (m) { return m.ThemeModule; }); }
            },
            {
                path: 'widgets',
                loadChildren: function () { return import('./views/widgets/widgets.module').then(function (m) { return m.WidgetsModule; }); }
            }
        ]
    },
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = tslib_1.__decorate([
        NgModule({
            imports: [RouterModule.forRoot(routes)],
            exports: [RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
export { AppRoutingModule };
//# sourceMappingURL=app.routing.js.map