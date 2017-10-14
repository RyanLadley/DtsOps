import { Component, OnInit, Input } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'ticket-entry',
    templateUrl: './ticket-entry.template.html'
})
export class TicketEntryComponent implements OnInit {

    @Input() accounts: any[]
    @Input() vendors: any[]
    tickets: TicketForm[];

    constructor(private _serverRequest: ServerRequest) {

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
    
}
