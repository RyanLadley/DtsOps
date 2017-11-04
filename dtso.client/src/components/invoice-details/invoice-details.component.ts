import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { InvoiceForm } from '../../models/index';

@Component({
    selector: 'invoice-details',
    templateUrl: './invoice-details.template.html'
})
export class InvoiceDetailsComponent implements OnInit {

    displayedBreakdown: string
    editBasics: boolean;
    editTable: boolean;
    tempInvoice: any;
    invoice: any;

    //Edit Data
    invoiceTypes: any[];
    vendors: any[];
    cityAccounts: any[];
    accounts: any[];
    constructor(private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getInvoice(urlId);
        this.displayedBreakdown = "Expenses";
        this.editBasics = false;
        this.editTable = false;
        
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;
        this.toggleEditTable(false);
    }

    toggleEditBasics(editing: boolean) {
        
        this.editBasics = editing;
        if (editing) {
            this.getEditData();
            this.tempInvoice = JSON.parse(JSON.stringify(this.invoice));
        }
        else {
            this.invoice = JSON.parse(JSON.stringify(this.tempInvoice));
        }
    }

    toggleEditTable(editing: boolean) {
        //This is so when we switch tabs, we can call the toggle without issue
        if (!this.editTable && !editing)
            return;
        
        this.editTable = editing;
        if (editing) {
            this.getEditData();
            this.tempInvoice = JSON.parse(JSON.stringify(this.invoice));
        }
        else {
            this.invoice = JSON.parse(JSON.stringify(this.tempInvoice));
        }
    }

    isEditing(): boolean{
        return this.editBasics || this.editTable;
    }

    getInvoice(id: string) {
        this._server.get('api/invoice/' + id).subscribe(
            response => {
                this.invoice = response; console.log(response);
            },
            error => { }
        )
    }

    getEditData() {
        if (!this.invoiceTypes) {
            this.getInvoiceTypes();
        }

        if (!this.vendors) {
            this.getVendors();
        }

        if (!this.accounts) {
            this.getAccounts();
        }

        if (!this.cityAccounts) {
            this.getCityAccounts();
        }
    }

    getInvoiceTypes() {
        this._server.get('api/invoice/types').subscribe(
            response => { this.invoiceTypes = response },
            error => { }
        )
    }

    getVendors() {
        this._server.get('api/vendor').subscribe(
            response => { this.vendors = response },
            error => { }
        )
    }

    getCityAccounts() {
        this._server.get('api/account/city').subscribe(
            response => { this.cityAccounts = response },
            error => { }
        )
    }

    getAccounts() {
        this._server.get('api/account').subscribe(
            response => { this.accounts = response },
            error => { }
        )
    }

    parseDate(dateString: string): Date {
        if (dateString) {
            return new Date(dateString);
        } else {
            return null;
        }
    }

    submitAdjustment() {
        var invoiceForm = InvoiceForm.MapFromDetials(this.invoice);

        this._server.post('api/invoice/edit', invoiceForm).subscribe(
            response => {
                console.log(response);
                this.invoice = response;
                this.editTable = false;
                this.editBasics = false;
            },
            error => { }
        )
    }
}
