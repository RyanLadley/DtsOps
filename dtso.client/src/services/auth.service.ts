import { Injectable, EventEmitter} from '@angular/core';
import { Router } from '@angular/router'
import { Subject } from 'rxjs/Subject';
import { CookieManager } from '../services/cookie-manager.service';

@Injectable()
export class AuthService{
    loginUpdated: Subject<boolean> = new Subject<boolean>();
    isLoggedIn: boolean;
    user: any;
    constructor(private _cookieManager: CookieManager, private router: Router) {
    }


    login(token: any) {
        this.isLoggedIn = true;
        this._cookieManager.setToken(token.accessToken, token.expires);
        this._cookieManager.setUser(token.user, token.expires);

        if (this.router.url === "/login") {
            this.router.navigate(["/"]);
        }
        this.loginUpdated.next(true);
    }

    logout() {
        this.isLoggedIn = false;
        this._cookieManager.removeAll();
        this.user = undefined;
        this.router.navigate(["/login"]);

        this.loginUpdated.next(false);
    }

    getToken() {
        let token = this._cookieManager.getToken();

        if (!token && this.router.url != "/login")
            this.logout();
        else
            return token;
    }

    getPermissions() {
        if(this.user == undefined){
            this.user = this._cookieManager.getUser();
        }

        return this.user.permissions;
    }

    getFirstName() {
        if (this.user == undefined) {
            this.user = this._cookieManager.getUser();
        }

        return this.user.firstName;
    }

    getFullName() {
        if (this.user == undefined) {
            this.user = this._cookieManager.getUser();
        }

        return this.user.firstName + " " +this.user.lastName;
    }
}