import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthProvider,ServerRequest } from '../../services/index';

@Component({
    selector: 'account-details',
    templateUrl: './account-details.template.html'
})
export class AccountDetailsComponent implements OnInit{
    collapse: boolean;
    selectedMonth: any;
    displayedBreakdown: string
    displayedInvoices: any = [];
    displayedTickets: any = [];
    months: string[]
    account: any;


    constructor(private _monthProvider: MonthProvider, private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.months = this._monthProvider.monthList();
        //Default to Current Month
        this.selectedMonth = new Date().getMonth();
        this.setDisplayedBreakdown('Invoice');
        this.getAccount(urlId);

    }

    getAccount(id: string) {
        this._server.get('api/account/' + id).subscribe(
            response => {
                this.account = response; console.log(response);
                this.selectMonth(this.selectedMonth);
            },
            error => { }
        )
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;
    }

    selectMonth(month: any) {
        this.selectedMonth = month;
        if (month == 'All') {
            this.displayedInvoices = [];
            this.displayedTickets = [];
            for (var i = 0; i < this.months.length; i++) {
                this.displayedInvoices = this.displayedInvoices.concat(this.account.monthlyDetails[i+1].invoices);
                this.displayedTickets = this.displayedTickets.concat(this.account.monthlyDetails[i + 1].tickets);
            }
        }
        //An Individual Month was selected
        else {
            this.displayedInvoices = this.account.monthlyDetails[this.selectedMonth + 1].invoices
            this.displayedTickets = this.account.monthlyDetails[this.selectedMonth + 1].tickets
        }
    }
    
}
