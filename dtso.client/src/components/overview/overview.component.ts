import { Component, OnInit } from '@angular/core';
import { ServerRequest } from '../../services/index';

@Component({
    selector: 'overview',
    templateUrl: './overview.template.html'
})
export class OverviewComponent implements OnInit {
    collapse: boolean;
    accounts: any;

    constructor(private _server: ServerRequest) {

    }

    ngOnInit() {
        this.collapse = false;
        this.accounts = this.getAccounts();

        this.toggleCollapse();
    }

    toggleCollapse() {
        if (this.accounts) {
            this.collapse = !this.collapse;

            for (var i = 0; i < this.accounts.length; i++) {
                this.accounts[i].hideChildren = this.collapse;

                for (var j = 0; j < this.accounts[i].childAccounts.length; j++) {
                    this.accounts[i].childAccounts[j].hideChildren = this.collapse;
                }
            }
        }
    }


    getAccounts() {
        this._server.get('api/account/overview').subscribe(
            response => { this.accounts = response },
            error => { }
        )
    }

    
}
