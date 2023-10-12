import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from '../services/auth.service';

import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        // const currentUser = this.authenticationService.currentUser();
        // if (currentUser) {
        //     // logged in so return true
        //     return true;
        // }

        // const roles = JSON.parse(localStorage.getItem('roles'));
        // console.log('roles', roles);


        // if (localStorage.getItem('roles') === '"Vendor"') {
        //     this.router.navigate(['/vendors/files/' + localStorage.getItem('userId') + '/view']);
        // } else {
        //     this.router.navigate(['/users/users']);
        // }

        if (this.authService.loggedIn()) {
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
