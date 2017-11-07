import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'ticket-details',
    templateUrl: './ticket-details.template.html'
})
export class TicketDetailsComponent implements OnInit {
    editBasics: boolean;

    ticket: any;
    tempTicket: any;

    vendors: any[];
    accounts: any[];
    materials: any[];

    constructor(private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getTicket(urlId);
    }

    getTicket(urlId) {
        this._server.get('api/ticket/' + urlId).subscribe(
            response => { this.ticket = response },
            error => { }
        )
    }

    toggleEditBasics(editing: boolean) {

        this.editBasics = editing;
        if (editing) {
            this.getEditData();
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
            response => { this.materials = response; },
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

        this._server.post('api/ticket/edit', ticketForm).subscribe(
            response => {
                this.ticket = response;
                this.editBasics = false;
            },
            error => { }
        )
    }
}
