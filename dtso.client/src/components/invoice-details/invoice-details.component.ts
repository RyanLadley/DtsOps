import { Component, OnInit } from '@angular/core';
import { MonthProvider} from '../../services/index';
@Component({
    selector: 'invoice-details',
    templateUrl: './invoice-details.template.html'
})
export class InvoiceDetailsComponent implements OnInit {

    displayedBreakdown: string

    constructor() {

    }

    ngOnInit() {
        this.displayedBreakdown = "Expenses";
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;
    }

    invoice: any = {
        "invoiceId": 2,
        "invoiceNumber": "17POS/092790A",
        "vendor": {
            "vendorId": 1,
            "name": "Grainger"
        },
        "invoiceType": "Equipment",
        "invoiceDate": "2017-08-23T12:30:00",
        "datePaid": "2017-09-23T12:30:00",
        "description": "Donec vestibulum convallis tortor",
        "totalExpense" : 850.23,
        "expenses": [
            {
                "account": {
                    "accountId": 4,
                    "accountNumber": 521000,
                    "subNo": 2,
                    "shredNo" : 5
                },
                "expense": 40.56
            },
            {
                "account": {
                    "accountId": 4,
                    "accountNumber": 523000,
                    "subNo": 2,
                    "shredNo": 5
                },
                "expense": 698.45
            }
        ],
        "tickets": [
            {
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
                    "name": "Big ole 2x4 Extra Woody"
                },
                "cost": 12.00
            },
            {
                "ticketId": 3,
                "ticketNumber": "6EGHS4231ASF",
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
                    "name": "Big ole 2x4 Extra Woody"
                },
                "cost": 12.00
            }
        ]
    };
}
