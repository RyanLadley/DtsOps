import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest, AuthService } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'ticket-details',
    templateUrl: './ticket-details.template.html'
})
export class TicketDetailsComponent implements OnInit {
    editBasics: boolean;

    ticket: any;
    tempTicket: any;

    //Datepicker value
    ticketDate: any;

    vendors: any[];
    accounts: any[];
    materials: any[];
    permissions: string;
    errorMessage: string;

    constructor(private _authService: AuthService, private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getTicket(urlId);
        this.permissions = this._authService.getPermissions();
    }

    getTicket(urlId) {
        this._server.get('api/ticket/' + urlId).subscribe(
            response => { this.ticket = response; },
            error => { }
        )
    }

    toggleEditBasics(editing: boolean) {

        this.editBasics = editing;
        if (editing) {
            this.getEditData();

            //Initiazlie Date Picker Dates
            let date = new Date(this.ticket.date)
            this.ticketDate = { date: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() } };

            this.tempTicket = JSON.parse(JSON.stringify(this.ticket));
        }
        else {
            this.ticket = JSON.parse(JSON.stringify(this.tempTicket));
        }
    }

    removeInvoice() {
        this.ticket.invoice = null;
    }

    getEditData() {
        if (!this.vendors) {
            this.getVendors();
        }

        if (!this.accounts) {
            this.getAccounts();
        }

        if (!this.materials) {
            this.getMaterials(this.ticket.vendor.vendorId);
        }
    }

    getVendors() {
        this._server.get('api/vendor?withMaterials=true').subscribe(
            response => { this.vendors = response;},
            error => { }
        )
    }

    getAccounts() {
        this._server.get('api/account').subscribe(
            response => { this.accounts = response },
            error => { }
        )
    }

    getMaterials(vendorId: number) {
        this._server.get('api/material/vendor/' + vendorId).subscribe(
            response => { this.materials = response;},
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

    submitTicketAdjustment(){
        var ticketForm = TicketForm.MapFromDetails(this.ticket);

        if (this.ticketDate == null) {
            this.errorMessage = "A ticket date is required.";
            return
        }
        ticketForm.date = new Date(this.ticketDate.date.year, this.ticketDate.date.month - 1, this.ticketDate.date.day);

        this._server.post('api/ticket/edit', ticketForm).subscribe(
            response => {
                this.ticket = response;
                this.editBasics = false;
                this.errorMessage = null;
            },
            error => { this.errorMessage = error }
        )
    }
}
