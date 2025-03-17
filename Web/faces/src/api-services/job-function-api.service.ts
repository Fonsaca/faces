import { Injectable } from '@angular/core';
import { JobFunction } from '../shared/models/job-function';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environment';
import { firstValueFrom, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobFunctionApiService {

  constructor(private httpClient: HttpClient) { }


  async getJobFunctions() : Promise<JobFunction[]>{

    const url = `${environment.api}/api/jobfunction`;

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
