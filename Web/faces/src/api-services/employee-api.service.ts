import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { catchError, firstValueFrom, map } from 'rxjs';
import { Employee } from '../shared/models/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeApiService {

  constructor(private httpClient: HttpClient) { }


  async getEmployee(document:string) : Promise<Employee>{

    const url = `${environment.api}/api/employee/${document}`;

    const request = this.httpClient.get(url)
      .pipe(map((response : any) => {

        console.log(response)

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

        console.log(response)

        if(response?.statusCode != 200)
          throw response

        return response.data;
      }))

      return firstValueFrom(request);
  }
}
