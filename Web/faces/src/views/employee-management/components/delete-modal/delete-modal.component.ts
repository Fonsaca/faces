import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { Employee } from '../../../../shared/models/employee';
import { EmployeeApiService } from '../../../../api-services/employee-api.service';
import { MessageService } from 'primeng/api';
import { PromiseState } from '../../../../shared/PromiseState';

@Component({
  selector: 'delete-modal',
  standalone: false,
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})
export class DeleteModalComponent {

  iconConfirm = faCheck

  constructor(private employeeApi: EmployeeApiService, private notify: MessageService){}

  @Input()
  showingModal = false;
  
  @Output()
  showingModalChange = new EventEmitter<boolean>();

  @Input()
  employee?: Employee;
  
  get modalVisibility() {
    return this.showingModal;
  }
  set modalVisibility(value: boolean) {
    this.showingModalChange.emit(value)
  }

  loading = signal(false);

  async deleteEmployee(id: number){

    var request = new PromiseState(this.employeeApi.delete(id))
   
    request.loadingSignal(this.loading);

    var result = await request.awaitSafe();

    if(result.isSuccess){
      this.notify.add({
        detail: 'Employee deleted successfuly',
        severity: 'success',
        summary: 'Success'
      })
      this.showingModalChange.emit(false)
    } else{
      this.notify.add({
        detail: result.error ?? 'Error trying to delete the employee',
        severity: 'error',
        summary: 'Error'
      })


    }
  }

}
