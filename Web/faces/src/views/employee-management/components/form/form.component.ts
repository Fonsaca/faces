import { Component, EventEmitter, Input, OnInit, Output, signal, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { JobFunctionApiService } from '../../../../api-services/job-function-api.service';
import { PromiseState } from '../../../../shared/PromiseState';
import { JobFunction } from '../../../../shared/models/job-function';
import { faPlus, faSave, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { Employee } from '../../../../shared/models/employee';
import { EmployeeApiService } from '../../../../api-services/employee-api.service';

@Component({
  selector: 'emp-man-form',
  standalone: false,
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent implements OnInit{

  iconSave = faSave
  iconAdd = faPlus
  iconRemove = faTrashAlt

  constructor(private fb: FormBuilder, 
    private notify: MessageService, 
    private jobFunctionApi: JobFunctionApiService,
    private employeeApi: EmployeeApiService){
    
    this.form = fb.group({
      id: fb.control(0),
      docNumber: fb.control('',Validators.required),
      firstName: fb.control('',Validators.required),
      lastName: fb.control('',Validators.required),
      email: fb.control('',Validators.required),
      birthDate: fb.control('',Validators.required),
      password: fb.control(''),
      confirmPassword: fb.control(''),
      jobFunction: fb.control(undefined,Validators.required),
      manager: fb.control(undefined),
      phones: fb.array([])
    })

  }

  @Input() employee?: Employee;

  @Input() employees: Employee[] = [];

  @Output() saved = new EventEmitter<void>();
  
  form!: FormGroup;

  jobFunctions: JobFunction[] = [];

  loading = signal(false);

  get isNewEmployee(): boolean{
    return this.employee == null;
  }

  get phonesArray(): FormArray{ 
    return (this.form.controls['phones'] as FormArray)
  }

  get managers(): Employee[] {
    return this.employees.filter(e => e.id != this.employee?.id)
  }
  
  async ngOnInit(): Promise<void> {
    await this.requestJobFunctions();
  }

  ngOnChanges(changes: SimpleChanges){
    const employee = changes["employee"];

    if(employee?.currentValue){
      this.form.patchValue(employee.currentValue)

      const birthDate = new Date(employee.currentValue.birthDate)
      const userTimezoneOffset = birthDate.getTimezoneOffset() * 60000;
      
      this.form.patchValue({
        birthDate: new Date(birthDate.getTime() + userTimezoneOffset)
      });

      for(let phone of employee.currentValue.phones){
        const phoneForm = this.fb.group({
            id: this.fb.control(phone.id),
            label: this.fb.control(phone.label, Validators.required),
            number: this.fb.control(phone.number, Validators.required)
        });

        phoneForm.controls['label'].disable();
        phoneForm.controls['number'].disable();
      
        (this.form.controls['phones'] as FormArray).push(phoneForm);
      }

      this.form.controls['docNumber'].disable();

    }

  }

  addPhone() {
    const phoneForm = this.fb.group({
        id: this.fb.control(0),
        label: this.fb.control('', Validators.required),
        number: this.fb.control('', Validators.required)
    });
  
    (this.form.controls['phones'] as FormArray).push(phoneForm);
  }

  deletePhone(phoneIdx: number) {
    (this.form.controls['phones'] as FormArray).removeAt(phoneIdx);
  }


  async save(form: FormGroup){

    if(form.invalid){
      this.notify.add({
        detail: 'Complete the required fields',
        severity: 'warning',
        summary: 'Warning'
      })
      return;
    }

    if(this.isNewEmployee){
      const password = form.value.password;
      const confirmPassword = form.value.confirmPassword;

      if(confirmPassword != password){
        this.notify.add({
          detail: 'Passwords do not match',
          severity: 'warning',
          summary: 'Warning'
        })
        return;
      }
    }

    const employee = form.getRawValue() as Employee;

    if(this.isNewEmployee)
      await this.createEmployee(employee);
    else
      await this.editEmployee(employee);

  }


  private async requestJobFunctions() : Promise<void>{

    var request = new PromiseState(this.jobFunctionApi.getJobFunctions())

    // request.loadingSignal(this.loading);

    var result = await request.awaitSafe();

    if(result.isSuccess){
      this.jobFunctions = result.data!
    } else{
      this.notify.add({
        detail: result.error ?? 'Error to get job functions',
        severity: 'error',
        summary: 'Error'
      })


    }

  }

  private async createEmployee(employee: Employee) : Promise<void>{

    var request = new PromiseState(this.employeeApi.create(employee))

    request.loadingSignal(this.loading);

    var result = await request.awaitSafe();

    if(result.isSuccess){
      this.notify.add({
        detail: 'Employee created successfuly',
        severity: 'success',
        summary: 'Success'
      })
      this.saved.emit()
    } else{
      this.notify.add({
        detail: result.error ?? 'Error trying to create an employee',
        severity: 'error',
        summary: 'Error'
      })


    }

  }

  private async editEmployee(employee: Employee) : Promise<void>{

    var request = new PromiseState(this.employeeApi.edit(employee))

    request.loadingSignal(this.loading);

    var result = await request.awaitSafe();

    if(result.isSuccess){
      this.notify.add({
        detail: 'Employee updated successfuly',
        severity: 'success',
        summary: 'Success'
      })
      this.saved.emit()
    } else{
      this.notify.add({
        detail: result.error ?? 'Error trying to update the employee',
        severity: 'error',
        summary: 'Error'
      })


    }

  }
}
