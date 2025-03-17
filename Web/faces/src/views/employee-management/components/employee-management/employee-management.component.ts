import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Employee } from '../../../../shared/models/employee';
import { EmployeeApiService } from '../../../../api-services/employee-api.service';
import { Subject } from 'rxjs';
import { PromiseState } from '../../../../shared/PromiseState';
import { MessageService } from 'primeng/api';
import { faPencilAlt, faPlus, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { JobFunction } from '../../../../shared/models/job-function';
import { JobFunctionApiService } from '../../../../api-services/job-function-api.service';


@Component({
  selector: 'app-employee-management',
  standalone: false,
  templateUrl: './employee-management.component.html',
  styleUrl: './employee-management.component.css'
})
export class EmployeeManagementComponent implements OnInit{

  iconNew = faPlus
  iconEdit = faPencilAlt
  iconDelete = faTrashAlt
  
  constructor(private employeeApi: EmployeeApiService, private notify: MessageService){}

  employees: Employee[] = [];
  

  loading = signal(false);

  showingFormsModal = false;

  showingDeleteModal = false;

  selectedDeleteEmployee?: Employee;

  selectedFormsEmployee?: Employee;

  async ngOnInit(): Promise<void> {
    await this.requestEmployees();
  }

  openFormsModal(employee?: Employee){
    this.selectedFormsEmployee = employee;
    this.showingFormsModal = true;
  }

  openDeleteModal(employee: Employee){
    this.selectedDeleteEmployee = employee;
    this.showingDeleteModal = true;
  }


  async requestEmployees() : Promise<void>{

    var request = new PromiseState(this.employeeApi.getEmployees())

    request.loadingSignal(this.loading);

    var result = await request.awaitSafe();

    if(result.isSuccess){
      this.employees = result.data!
    } else{
      this.notify.add({
        detail: result.error ?? 'Error to get employees',
        severity: 'error',
        summary: 'Error'
      })


    }

  }

  
  

}
