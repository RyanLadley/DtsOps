import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { InvoiceForm, InvoiceAccount } from '../../models/index';

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
    pendingTickets: any[]

    cityExpenseToRemove: number[] = [];
    invoiceAccountsToRemove: number[] = [];
    //Edit Data
    invoiceTypes: any[];
    vendors: any[];
    vendorsWithMaterial: any[];
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
            //If we are editing tickets, always get pending just incase the user altered the vendor id in a previous edit
            if (this.displayedBreakdown == "Tickets") {
                this.getPendingTickets();
            }

            this.tempInvoice = JSON.parse(JSON.stringify(this.invoice));
        }
        else {
            this.invoice = JSON.parse(JSON.stringify(this.tempInvoice));
        }
    }

    isEditing(): boolean{
        return this.editBasics || this.editTable;
    }

    addAccount() {
        this.invoice.expenses.push({ account: {}, expense: 0, cityExpense: [] });
    }

    removeAccount(index: number) {
        if (this.invoice.expenses[index].invoiceAccountId) {
            this.invoiceAccountsToRemove.push(this.invoice.expenses[index].invoiceAccountId)
        }

        this.invoice.expenses.splice(index, 1);
    }

    addTicket(index: number) {
        this.invoice.tickets.push(this.pendingTickets[index]);
        this.pendingTickets.splice(index, 1);
    }

    removeTicket(index: number) {
        this.pendingTickets.push(this.invoice.tickets[index])
        this.invoice.tickets.splice(index, 1);
    }

    addCityAccount(accountIndex: number) {
        this.invoice.expenses[accountIndex].cityExpense.push({ cityAccountId: undefined, expense: 0});
    }

    removeCityAccount(accountIndex: number, cityAccountIndex: number) {
        if (this.invoice.expenses[accountIndex].cityExpense[cityAccountIndex].cityExpenseId) {
            this.cityExpenseToRemove.push(this.invoice.expenses[accountIndex].cityExpense[cityAccountIndex].cityExpenseId)
        }

        this.invoice.expenses[accountIndex].cityExpense.splice(cityAccountIndex, 1);
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

    getPendingTickets() {
        this._server.get('api/ticket/vendor/' + this.invoice.vendor.vendorId +"?onlyPending=true").subscribe(
            response => { this.pendingTickets = response },
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
        //This means we are edinting tickets, se we have to send a diffrent call
        console.log(this.invoice)
        if (this.editTable && this.displayedBreakdown == "Tickets") {
            this.submitTicketAdjustments();
        }
        else {
            var invoiceForm = InvoiceForm.MapFromDetials(this.invoice);

            invoiceForm.cityExpensesToRemove = this.cityExpenseToRemove;
            invoiceForm.invoiceAccountsToRemove = this.invoiceAccountsToRemove;

            this._server.post('api/invoice/edit', invoiceForm).subscribe(
                response => {
                    this.invoice = response;
                    this.editTable = false;
                    this.editBasics = false;
                },
                error => { }
            )
        }
    }

    submitTicketAdjustments() {
        //We only need the tickets ids so exctract theses
        var ticketIds = [];
        for (var i = 0; i < this.invoice.tickets.length; i++) {
            ticketIds.push(this.invoice.tickets[i].ticketId);
        }

        //Create a form to send to the server
        var invoiceForm = {
            invoiceId: this.invoice.invoiceId,
            ticketIds: ticketIds
        }

        this._server.post('api/invoice/tickets', invoiceForm).subscribe(
            response => {
                this.invoice = response;
                this.editTable = false;
                this.editBasics = false;
            },
            error => { }
        )

    }
}
