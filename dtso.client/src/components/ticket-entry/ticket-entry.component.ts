import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ServerRequest } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'ticket-entry',
    templateUrl: './ticket-entry.template.html'
})
export class TicketEntryComponent implements OnInit {
    errorMessage: string;

    @Input() accounts: any[]
    vendors: any[]
    tickets: TicketForm[];
    materials: any;
    vendorId: number;
    accountId: number;

    constructor(private _server: ServerRequest, private _router: Router) {
        this.getVendorsWithMaterial();
    }

    ngOnInit() {
        this.tickets = [new TicketForm()];
    }

    addTicket() {
        this.tickets.push(new TicketForm());
    }

    removeTicket(index) {
        this.tickets.splice(index, 1);
    }

    getMaterials(vendorId: number) {
        this._server.get('api/material/vendor/' + vendorId).subscribe(
            response => { this.materials = response;},
            error => { }
        )
    }

    getVendorsWithMaterial() {
        this._server.get('api/vendor?withMaterials=true').subscribe(
            response => { this.vendors = response; },
            error => { }
        )
    }

    selectTicketMaterial(materialId: number, index: number) {

        for (var i = 0; i < this.materials.length; i++) {
            if (this.materials[i].materialVendorId == materialId) {
                this.tickets[index].material = this.materials[i];
            }
        }
    }

    caluculateCost(ticket: TicketForm) {
        ticket.cost = ticket.quantity * ticket.material.cost;
        console.log(ticket.cost)
    }

    submitNewTickets() {
        //Add validation and stuff. Have fun future me
        
        if (!this.vendorId)
            return;
        
        //Assign the selected accpunt and vendor ids to the tickets
        for (var i = 0; i < this.tickets.length; i++){
            this.tickets[i].accountId = this.accountId;
            this.tickets[i].vendorId = this.vendorId;

            this.tickets[i].date = new Date(this.tickets[i].rawDate.date.year, this.tickets[i].rawDate.date.month - 1, this.tickets[i].rawDate.date.day);
        }

        this._server.post("api/ticket", this.tickets).subscribe(
            response => { this._router.navigate(['/vendor', this.vendorId])},
            error => { this.errorMessage = error }
        )
    }
}
