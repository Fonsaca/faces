import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { JobFunctionApiService } from '../../../../api-services/job-function-api.service';
import { PromiseState } from '../../../../shared/PromiseState';
import { JobFunction } from '../../../../shared/models/job-function';

@Component({
  selector: 'emp-man-form',
  standalone: false,
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent implements OnInit{


  constructor(private fb: FormBuilder, private notify: MessageService, private jobFunctionApi: JobFunctionApiService){

    this.form = fb.group({
      docNumber: fb.control('',Validators.required),
      firstName: fb.control('',Validators.required),
      lastName: fb.control('',Validators.required),
      email: fb.control('',Validators.required),
      birthDate: fb.control('',Validators.required),
    })
  }


  form!: FormGroup;

  jobFunctions: JobFunction[] = [];

  
  async ngOnInit(): Promise<void> {
    await this.requestJobFunctions();
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
}
