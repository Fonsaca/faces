import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { EmployeeManagementComponent } from './components/employee-management/employee-management.component';
import { FormComponent } from './components/form/form.component';
import { DeleteModalComponent } from './components/delete-modal/delete-modal.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms'
import { ButtonModule } from 'primeng/button';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DialogModule } from 'primeng/dialog';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { LoadingComponent } from "../../shared/components/loading/loading.component";
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';

@NgModule({
  declarations: [
    EmployeeManagementComponent,
    FormComponent,
    DeleteModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    ButtonModule,
    FontAwesomeModule,
    DialogModule,
    FloatLabelModule,
    InputTextModule,
    LoadingComponent,
    SelectModule,
    DatePickerModule
]
})
export class EmployeeManagementModule { }
