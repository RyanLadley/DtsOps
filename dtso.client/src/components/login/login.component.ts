import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest, AuthService } from '../../services/index';

@Component({
  selector: 'login',
  templateUrl: './login.template.html'
})
export class LoginComponent implements OnInit {

    loginForm: any;
    errorMessage: string;

    constructor(private _server: ServerRequest, private _authService: AuthService, private _route: ActivatedRoute, private _router: Router) {
        
    }

    ngOnInit() {
        this.loginForm = {}
    }

    login() {
        this.errorMessage = "";
        this._server.post("api/user/login", this.loginForm).subscribe(
            response => { this._authService.login(response);},
            error => { this.errorMessage = error }
        )
    }
}
