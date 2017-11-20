import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService, CookieOptions } from 'ngx-cookie';

@Injectable()
export class CookieManager {

    tokenKey : string = "Token"
    userKey: string = "User"

    constructor(private _cookies : CookieService) {

    }

    setToken(token:string, expiration:any) {
        let options: CookieOptions = {
            expires: expiration
        }

        this._cookies.put(this.tokenKey, token, options);
    }
    
    getToken() : string{
        return this._cookies.get(this.tokenKey);
    }

    setUser(user: any, expiration: any) {
        let options: CookieOptions = {
            expires: expiration
        }

        this._cookies.putObject(this.userKey, user, options);
    }

    getUser(): any {
        return this._cookies.getObject(this.userKey);
    }

    removeAll() {
        this._cookies.removeAll();
    }
}