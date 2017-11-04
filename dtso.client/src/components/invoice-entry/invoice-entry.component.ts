import { Component, OnInit, Input } from '@angular/core';
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

    invoice: InvoiceForm;
    errorMessage: string;
    constructor(private _server: ServerRequest) {

    }
    ngOnInit(){
        this.invoice = new InvoiceForm();
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
        console.log(this.invoice);
        this._server.post("api/invoice", this.invoice).subscribe(
            response => {/*add navigation to the new invoice details */ },
            error => { this.errorMessage = error }
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
