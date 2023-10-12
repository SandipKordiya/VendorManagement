import { Injectable } from '@angular/core';
import { User } from '../models/auth.models';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ReplaySubject, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

// import { getFirebaseBackend } from '../../authUtils';


@Injectable({ providedIn: 'root' })

export class AuthenticationService {
    baseUrl = environment.apiUrl;
    jwtHelper = new JwtHelperService();
    private currentUserSource = new ReplaySubject<User>(1);
    currentUser$ = this.currentUserSource.asObservable();
    user: User;

    constructor(private http: HttpClient, private router: Router) {
    }

    /**
     * Returns the current user
     */
    public currentUser() {
        // return getFirebaseBackend().getAuthenticatedUser();
    }

    /**
     * Performs the auth
     * @param email email of user
     * @param password password of user
     */
    login(model: any) {
        return this.http.post(this.baseUrl + 'account/login', model).pipe(
            map((response: any) => {
                const result = response;
                console.log('response', response);
                if (result) {
                    localStorage.setItem('token', result.token);
                    localStorage.setItem('roles', JSON.stringify(result.roles));
                    localStorage.setItem('userId', result.id);
                    //   localStorage.setItem('user', JSON.stringify(result));
                    this.currentUserSource.next(result);
                }
            })
        );
    }

    loggedIn() {
        const token = localStorage.getItem('token');
        return !this.jwtHelper.isTokenExpired(token);
    }

    /**
     * Performs the register
     * @param email email
     * @param password password
     */
    register(email: string, password: string) {
        // return getFirebaseBackend().registerUser(email, password).then((response: any) => {
        //     const user = response;
        //     return user;
        // });
    }

    /**
     * Reset password
     * @param email email
     */
    resetPassword(email: string) {
        // return getFirebaseBackend().forgetPassword(email).then((response: any) => {
        //     const message = response.data;
        //     return message;
        // });
    }

    /**
     * Logout the user
     */
    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('roles');
        localStorage.removeItem('userId');
        this.currentUserSource.next(null);
        this.router.navigateByUrl('auth/login');
    }
}

