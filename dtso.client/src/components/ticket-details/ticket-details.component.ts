import { Component, OnInit, Input } from '@angular/core';
import { ServerRequest } from '../../services/index';
@Component({
    selector: 'ticket-details',
    templateUrl: './ticket-details.template.html'
})
export class TicketDetailsComponent implements OnInit {
    
    constructor(private _serverRequest: ServerRequest) {

    }

    ngOnInit() {

    }
    
    ticket = {
        "ticketId": 1,
        "ticketNumber": "657894231ASF",
        "vendor": {
            "vendorId": 1,
            "name": "Grainger"
        },
        "account": {
            "accountId": 4,
            "accountNumber": 523000,
            "subNo": 2,
            "shredNo": 5
        },
        "material": {
            "materialId": 2,
            "name": "Big ole 2x4 Extra Woody",
            "amount": 2.6,
            "unit": "Ton"
        },
        "invoice": {
            "invoiceId": 1,
            "invoiceNumber": "ASF12986"
        },
        "cost": 12.00
    }
}
