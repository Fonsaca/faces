import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { SessionService } from "../services/session.service";
import { catchError, Observable, throwError } from "rxjs";


export const globalHttpInterceptor = () : HttpInterceptorFn => {
    
    return (request: HttpRequest<unknown>, next: HttpHandlerFn) : Observable<HttpEvent<unknown>> => {
        const session = inject(SessionService);

        
        const token = session.token.getValue();
        let headers = request.headers.set('Authorization', `bearer ${token}` );


        if(!headers.has('Content-Type'))
            headers = headers.set('Content-Type', 'application/json')

        return next(request.clone({ headers }))
            .pipe(catchError((e:any)=>{
                if(e && e.status && e.status == 401){
                    session.logout();
                }
                return throwError(() => e)
            }))
    
    }
}