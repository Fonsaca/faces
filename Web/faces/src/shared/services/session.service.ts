import { inject, Injectable, WritableSignal } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, skip } from 'rxjs';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private router: Router) { 
    this.token = new BehaviorSubject<string>('');
    this.registerTokenChange();
    
  }


  token! :BehaviorSubject<string> 

  employee = new BehaviorSubject<Employee|undefined>(undefined);


  logout(): void{
    this.token.next('');
    this.employee.next(undefined);
    this.router.navigateByUrl('login');
  }

  isValidToken(): boolean {

    const token = this.token.getValue();

    if(!token)
      return false;

    const decoded = this.decodeJWT(token);


    return new Date() > new Date(decoded.exp);
  }

  decodeJWT(token: string) : { sub: string, name: string, exp: number} {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(atob(base64));
  }


  private registerTokenChange(){
    this.token.pipe(skip(1)).subscribe(token => {
      localStorage.setItem('token',token);
    })
  }

 
}
