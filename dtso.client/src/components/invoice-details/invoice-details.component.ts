import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest, AuthService, ArraySort } from '../../services/index';
import { InvoiceForm, InvoiceAccount } from '../../models/index';
import { AppSettings } from "../../settings/appsettings";

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
    permissions: string;
    errorMessage: string;
    cityExpenseToRemove: number[] = [];
    invoiceAccountsToRemove: number[] = [];
    //Edit Data
    invoiceTypes: any[];
    vendors: any[];
    vendorsWithMaterial: any[];
    cityAccounts: any[];
    accounts: any[];

    //Datepicker Values
    invoiceDate: any;
    datePaid: any;

    sortExpensesAscending: boolean;
    currentExpenseSort: string;

    sortTicketAscending: boolean;
    currentTicketSort: string;

    constructor(private _authService: AuthService, private _route: ActivatedRoute, private _router: Router, private _server: ServerRequest, private _appSettings : AppSettings, private _sorter: ArraySort) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });
        
        this.currentTicketSort = "account";
        this.currentExpenseSort = "account";

        this.getInvoice(urlId);
        this.displayedBreakdown = "Expenses";
        this.editBasics = false;
        this.editTable = false;
        this.permissions = this._authService.getPermissions();
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;
        this.toggleEditTable(false);
    }

    toggleEditBasics(editing: boolean) {

        this.errorMessage = null;
        this.editBasics = editing;
        if (editing) {
            if (this.editTable) {
                this.toggleEditTable(false);
            }

            //Initiazlie Date Picker Dates
            let inDate = new Date(this.invoice.invoiceDate)
            this.invoiceDate = { date: { year: inDate.getFullYear(), month: inDate.getMonth() + 1, day: inDate.getDate() } };
            let paid = new Date(this.invoice.datePaid)
            this.datePaid = { date: { year: paid.getFullYear(), month: paid.getMonth() + 1, day: paid.getDate() } };

            this.getEditData();
            this.tempInvoice = JSON.parse(JSON.stringify(this.invoice));
        }
        else {
            this.invoice = JSON.parse(JSON.stringify(this.tempInvoice));
        }
    }

    toggleEditTable(editing: boolean) {

        this.errorMessage = null;
        //This is so when we switch tabs, we can call the toggle without issue
        if (!this.editTable && !editing)
            return;
        
        this.editTable = editing;
        if (editing) {
            if (this.editBasics) {
                
                this.toggleEditBasics(false);
            }

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
                this.invoice = response;
                this.sortExpensesBy(this.currentExpenseSort);
                this.sortTicketsBy(this.currentTicketSort);
            },
            error => { }
        )
    }
    
    getCoversheet() {
        this._server.get('api/document/coversheet/invoice/' + this.invoice.invoiceId).subscribe(
            response => {
                window.open(this._appSettings.serverUrl + "Documents/" + response);
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

    //This is a function rather than a router link so that we can preform a check if we are editing: we dont want to leave if we are editing
    gotoTicket(ticketId, editTable) {
        if (!editTable) {
            this._router.navigate(['ticket/' + ticketId]);
        }
    }

    submitAdjustment() {
        //This means we are edinting tickets, se we have to send a diffrent call
        if (this.editTable && this.displayedBreakdown == "Tickets") {
            this.submitTicketAdjustments();
        }
        else {
            var invoiceForm = InvoiceForm.MapFromDetials(this.invoice);

            if (this.invoiceDate == null) {
                this.errorMessage = "An invoice date is required.";
                return
            }
            if (this.datePaid == null) {
                this.errorMessage = "A date paid date is required.";
                return
            }
            invoiceForm.invoiceDate = new Date(this.invoiceDate.date.year, this.invoiceDate.date.month - 1, this.invoiceDate.date.day);
            invoiceForm.datePaid = new Date(this.datePaid.date.year, this.datePaid.date.month - 1, this.datePaid.date.day);

            invoiceForm.cityExpensesToRemove = this.cityExpenseToRemove;
            invoiceForm.invoiceAccountsToRemove = this.invoiceAccountsToRemove;

            this._server.post('api/invoice/edit', invoiceForm).subscribe(
                response => {
                    this.invoice = response;
                    this.editTable = false;
                    this.editBasics = false;
                    this.errorMessage = null;
                },
                error => { this.errorMessage = error}
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
                this.errorMessage = null;
            },
            error => { this.errorMessage = error; }
        )

    }

    getExpenseSortIcon(field) {
        if (field == this.currentExpenseSort) {
            if (this.sortExpensesAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortExpensesBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentExpenseSort) {
            direction = !this.sortExpensesAscending;
        }
        else {
            direction = true;
        }

        this.currentExpenseSort = field;
        this.sortExpensesAscending = direction;

        this._sorter.sort(this.invoice.expenses, field, direction);
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

        this._sorter.sort(this.invoice.tickets, field, direction);
    }

}
