import { Injectable, EventEmitter} from '@angular/core';
import { Router } from '@angular/router'
import { Subject } from 'rxjs/Subject';
import { TokenManager } from '../services/token-manager.service';
import { ServerRequest } from '../services/server-request.service';

@Injectable()
export class AuthService{
    loginUpdated: Subject<boolean> = new Subject<boolean>();
    isLoggedIn: boolean;

    constructor(private _tokenManager: TokenManager, private router: Router, private _serverRequest: ServerRequest) {
        /*this._serverRequest.get('authenticate').subscribe(
            response => { this.loginUpdated.next(true);},
            error => this.router.navigate(["/login"])
        );*/
    }


    loginUser(token: any) {
        this.isLoggedIn = true;
        this._tokenManager.saveToken(token.accessToken);

        if (this.router.url === "/login") {
            this.router.navigate(["/"]);
        }
        this.loginUpdated.next(true);
    }

    logoutUser() {
        this.isLoggedIn = false;
        this._tokenManager.removeToken();
        
        this.router.navigate(["/login"]);

        this.loginUpdated.next(false);
    }
    
}