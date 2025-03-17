import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { catchError, firstValueFrom, map } from 'rxjs';
import { Employee } from '../shared/models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeApiService {

  constructor(private httpClient: HttpClient) { }


  async getEmployee(document:string) : Promise<Employee>{

    const url = `${environment.api}/api/employee/${document}`;

    const request = this.httpClient.get(url)
      .pipe(map((response : any) => {


        if(response?.statusCode != 200)
          throw response

        return response.data[0];
      }))

      return firstValueFrom(request);
  }

  async getEmployees() : Promise<Employee[]>{

    const url = `${environment.api}/api/employee`;

    const request = this.httpClient.get(url)
      .pipe(map((response : any) => {


        if(response?.statusCode != 200)
          throw response

        return response.data;
      }))

      return firstValueFrom(request);
  }

  async create(employee: Employee) : Promise<void>{

    const url = `${environment.api}/api/employee`;

    const employeeUpdate = this.parseEmployeeToUpdate(employee);

    const request = this.httpClient.post(url,employeeUpdate)
      .pipe(map((response : any) => {


        if(response?.statusCode != 201)
          throw response

        return response.data;
      }))

      return firstValueFrom(request);
  }

  
  async edit(employee: Employee) : Promise<void>{

    const url = `${environment.api}/api/employee`;

    const employeeUpdate = this.parseEmployeeToUpdate(employee);

    const request = this.httpClient.put(url,employeeUpdate)
      .pipe(map((response : any) => {


        if(response?.statusCode != 200)
          throw response

        return response.data;
      }))

      return firstValueFrom(request);
  }

  async delete(id: number) : Promise<void>{

    const url = `${environment.api}/api/employee/${id}`;

    const request = this.httpClient.delete(url)
      .pipe(map((response : any) => {


        if(response?.statusCode != 200)
          throw response

        return response.data;
      }))

      return firstValueFrom(request);
  }

  private parseEmployeeToUpdate(employee:Employee) : any{
    return {
      id: employee.id,
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      docNumber: employee.docNumber,
      password:employee.password ?? '',
      birthDate: employee.birthDate,
      jobFunctionCode: employee.jobFunction.code,
      managerID: employee.manager?.id,
      phones: employee.phones
    };
  }
}
