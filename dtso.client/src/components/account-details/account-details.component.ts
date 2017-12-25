import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthProvider, ServerRequest, ArraySort } from '../../services/index';

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
    displayedTransfers: any = [];
    months: string[]
    account: any;

    sortInvoiceAscending: boolean;
    currentInvoiceSort: string;

    sortTicketAscending: boolean;
    currentTicketSort: string;

    sortTransferAscending: boolean;
    currentTransferSort: string;

    constructor(private _monthProvider: MonthProvider, private _route: ActivatedRoute, private _server: ServerRequest, private _sorter: ArraySort) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        //
        this.sortTransferAscending = true;
        this.sortTicketAscending = true;
        this.sortInvoiceAscending = true;

        this.currentTicketSort = "account";
        this.currentInvoiceSort = "accountNumber";
        this.currentTransferSort = "fromAccount"

        this.months = this._monthProvider.monthList();
        //Default to Current Month
        this.selectedMonth = new Date().getMonth();
        this.setDisplayedBreakdown('Invoice');
        this.getAccount(urlId);

    }

    getAccount(id: string) {
        this._server.get('api/account/' + id).subscribe(
            response => {
                this.account = response;
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
                this.displayedTransfers = this.displayedTransfers.concat(this.account.monthlyDetails[i + 1].transfers);
            }
        }
        //An Individual Month was selected
        else {
            this.displayedInvoices = this.account.monthlyDetails[this.selectedMonth + 1].invoices
            this.displayedTickets = this.account.monthlyDetails[this.selectedMonth + 1].tickets
            this.displayedTransfers = this.account.monthlyDetails[this.selectedMonth + 1].transfers
        }
        //This keeps the asccending from chainging afterselecting new month
        this.sortTransferAscending = !this.sortTransferAscending;
        this.sortTicketAscending = !this.sortTicketAscending;
        this.sortInvoiceAscending = !this.sortInvoiceAscending;

        this.sortInvoicesBy(this.currentInvoiceSort);
        this.sortTicketsBy(this.currentTicketSort);
        this.sortTransferBy(this.currentTransferSort);
    }

    getInvoiceSortIcon(field) {
        if (field == this.currentInvoiceSort) {
            if (this.sortInvoiceAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortInvoicesBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentInvoiceSort) {
            direction = !this.sortInvoiceAscending
        }
        else {
            direction = true;
        }

        this.currentInvoiceSort = field;
        this.sortInvoiceAscending = direction;

        this._sorter.sort(this.displayedInvoices, field, direction);
    }

    getTicketSortIcon(field) {
        if (field == this.currentTicketSort) {
            if (this.sortTicketAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortTicketsBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentTicketSort) {
            direction = !this.sortTicketAscending
        }
        else {
            direction = true;
        }

        this.currentTicketSort = field;
        this.sortTicketAscending = direction;

        this._sorter.sort(this.displayedTickets, field, direction);
    }

    getTransferSortIcon(field) {
        if (field == this.currentTransferSort) {
            if (this.sortTransferAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortTransferBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentTransferSort) {
            direction = !this.sortTransferAscending;
        }
        else {
            direction = true;
        }

        this.currentTransferSort = field;
        this.sortTransferAscending = direction;

        this._sorter.sort(this.displayedTransfers, field, direction);
    }
}
