import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestMethod, RequestOptions, Request, Response } from '@angular/http';
import { Subject, Observable } from 'rxjs/Rx';
import { AuthService } from '../services/auth.service';
import { AppSettings } from '../settings/appsettings';

@Injectable()
export class ServerRequest {

    constructor(private _http: Http, private _authService: AuthService, private _appSettings: AppSettings) {

    }

    get(url: string, payload?: any, parameters?: any) {
        return this._request(RequestMethod.Get, url, payload, parameters);
    }

    post(url: string, payload?: any, parameters?: any) {
        return this._request(RequestMethod.Post, url, payload, parameters);
    }


    private _getHeaders() {
        let headers: Headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Cache-Control': 'no-cache',
            'Pragma': 'no-cache'
        });

        let token = this._authService.getToken()
        if (token) {
            headers.append('Authorization', 'Bearer ' + token)
        }

        return headers
    }

    private _request(httpMethod: RequestMethod, url: string, payload?: any, parameters?: any): Observable<any> {

        let requestOptions = new RequestOptions(Object.assign({
            method: httpMethod,
            url: this._appSettings.serverUrl + url,
            headers: this._getHeaders(),
            body: payload,
            params: parameters
        }));

        return this._http.request(new Request(requestOptions))
            .map(response => {return response.text() ? response.json() : {} })
            .catch((error) => {return this._handleError(error); })
    }

    private _handleError(error: Response) {
        if (error.status == 403) {
        
        }
        return Observable.throw(error.text() ? error.json() : {})
    }


}