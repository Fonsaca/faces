import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeManagementModule } from './employee-management.module';
import { EmployeeManagementComponent } from './components/employee-management/employee-management.component';

const routes: Routes = [
  {
    path:'',
    component: EmployeeManagementComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes), EmployeeManagementModule],
  exports: [RouterModule]
})
export class EmployeeManagementRoutingModule { }
