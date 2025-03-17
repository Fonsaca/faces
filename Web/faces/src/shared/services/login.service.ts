import { Injectable, WritableSignal } from '@angular/core';
import { AuthenticationApiService, FacesCredential } from '../../api-services/authentication-api.service';
import { SessionService } from './session.service';
import { MessageService } from 'primeng/api';
import { PromiseState } from '../PromiseState';
import { Route, Router } from '@angular/router';
import { EmployeeApiService } from '../../api-services/employee-api.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private authApi : AuthenticationApiService,
     private employeeApi: EmployeeApiService,
     private session: SessionService,
     private notify: MessageService,
     private router: Router
  ) { }

  initApp(): void{
    const token = this.loadToken();
    if(token)
      this.setToken(token);
  }

  async login(credentials : FacesCredential, loading: WritableSignal<boolean>) : Promise<void>{

    const promise = this.authApi.login(credentials);

    const state = new PromiseState(promise);

    state.loadingSignal(loading)

    const result = await state.awaitSafe();

    if(!result.isSuccess){
      this.notify.add({
        detail: result.error ?? 'Unexpected error while trying to login',
        severity: 'error',
        summary: 'Error',
      });
    } else{
      await this.setEmployeeAfter(result.data!, loading);
    }


  }

  private async setEmployeeAfter(token : string, loading: WritableSignal<boolean>) : Promise<void>{


    const setTokenResult = await this.setToken(token, loading);

    if(!setTokenResult.success){
      this.notify.add({
        detail: setTokenResult.error,
        severity: 'error',
        summary: 'Error',
      });
    } else{
      this.router.navigateByUrl('/');
    }
    


  }

  private async setToken(token: string, loading?: WritableSignal<boolean>) : Promise<{success: boolean, error?:string}>{
     
    this.session.token.next(token);

    const jwtDecoded = this.session.decodeJWT(token);
  
    const document = jwtDecoded.sub; //todo
    
    const promise = this.employeeApi.getEmployee(document);

    const state = new PromiseState(promise);

    if(loading)
      state.loadingSignal(loading)

    const result = await state.awaitSafe();

    if(!result.isSuccess || !result.data){
      this.session.token.next('');
      this.session.employee.next(undefined);

      return {
        success: result.isSuccess,
        error: result.error ?? `Error to find the employee with document ${document}`
      }

    } else{
      this.session.employee.next(result.data!);

      return {
        success: true
      }

    }

  }

  private loadToken(): string|undefined{
    return localStorage.getItem('token') ?? undefined;
  }
 
}
