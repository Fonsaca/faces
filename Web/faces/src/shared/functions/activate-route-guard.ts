import { booleanAttribute, inject } from "@angular/core";
import { CanActivateFn, Router, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { SessionService } from "../services/session.service";

export const activateRouteGuard = (): CanActivateFn => {

    return () : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree => {

        
        const session = inject(SessionService);
        const router = inject(Router);

        if(session.isValidToken()) return true;

        return router.parseUrl('login');


    }



}