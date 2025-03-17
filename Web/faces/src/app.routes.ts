import { Routes } from '@angular/router';
import { LoginComponent } from './views/authentication/components/login/login.component';
import { activateRouteGuard } from './shared/functions/activate-route-guard';

export const routes: Routes = [

    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '',
        canActivate: [activateRouteGuard()],
        loadChildren: () => import('./views/employee-management/employee-management-routing.module').then(x => x.EmployeeManagementRoutingModule)
    }


];
