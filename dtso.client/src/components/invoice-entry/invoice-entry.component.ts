import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { InvoiceForm, InvoiceAccount, CityAccount } from '../../models/index';

@Component({
    selector: 'invoice-entry',
    templateUrl: './invoice-entry.template.html'
})
export class InvoiceEntryComponent implements OnInit {

    @Input() accounts: any[]
    @Input() vendors: any[];
    @Input() invoiceTypes: any[];
    @Input() cityAccounts: any[];

    datePaid: any;
    invoiceDate: any;
    invoice: InvoiceForm;
    errorMessage: string;
    constructor(private _router: Router, private _server: ServerRequest) {

    }
    ngOnInit(){
        this.invoice = new InvoiceForm();

        let now = new Date();
        this.datePaid = { date: { year: now.getFullYear(), month: now.getMonth()+1, day: now.getDate() } };
        this.invoiceDate = { date: { year: now.getFullYear(), month: now.getMonth()+1, day: now.getDate() } };
    }

    addAccount() {
        this.invoice.invoiceAccounts.push(new InvoiceAccount());
    }

    removeAccount(index: number) {
        if (this.invoice.invoiceAccounts.length > 1) {
            this.invoice.invoiceAccounts.splice(index, 1);
        }
    }

    addCityAccount(accountIndex: number) {
        this.invoice.invoiceAccounts[accountIndex].cityAccounts.push(new CityAccount());
    }

    removeCityAccount(accountIndex: number, cityAccountIndex: number) {
        if (this.invoice.invoiceAccounts[accountIndex].cityAccounts.length > 1) {
            this.invoice.invoiceAccounts[accountIndex].cityAccounts.splice(cityAccountIndex, 1);
        }
    }

    submitNewInvoice() {
        //Add validation and stuff. Have fun future me
        this.invoice.datePaid = new Date(this.datePaid.date.year, this.datePaid.date.month-1, this.datePaid.date.day);
        this.invoice.invoiceDate = new Date(this.invoiceDate.date.year, this.invoiceDate.date.month-1, this.invoiceDate.date.day);
        console.log(this.invoice)
        this._server.post("api/invoice", this.invoice).subscribe(
            response => { this._router.navigate(['/invoice', response.invoiceId]); },
            error => { this.errorMessage = error; console.log(error) }
        )
    }

    //Called whenever an expense is changed in the template
    calculateAccountExpense(accountIndex: number) {
        let sum: number = 0;
        let cityAccounts = this.invoice.invoiceAccounts[accountIndex].cityAccounts;

        for (var i = 0; i < cityAccounts.length; i++) {
            sum += cityAccounts[i].expense;
        }

        this.invoice.invoiceAccounts[accountIndex].expense = sum;
    }
}
