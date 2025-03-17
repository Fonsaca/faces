import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, firstValueFrom, map } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService {

  constructor(private httpClient: HttpClient) { }


  async login(credentials: FacesCredential) : Promise<string>{

    const url = `${environment.api}/api/authentication`;

    const request = this.httpClient.post(url,credentials)
      .pipe(map((response : any) => {
        if(response?.statusCode != 200)
          throw response

        return response.data[0]
      }))

      return firstValueFrom(request);
  }


}

export interface FacesCredential{

  document: string;

  password: string;

}
