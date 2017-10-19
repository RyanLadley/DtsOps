import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest } from '../../services/index';

@Component({
  selector: 'vendor-details',
  templateUrl: './vendor-details.template.html'
})
export class VendorDetailsComponent implements OnInit {

    displayedBreakdown: string;

    constructor(private _route: ActivatedRoute, private _router: Router) {
        
    }

    ngOnInit() {
        this.displayedBreakdown = "Invoice";
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;
    }

    selectFilter(filter: string) {
        
    }

    getStatusIcon(status: string) {
    }

    vendor: any =
        {
            "name": "Flynn",
            "contractNumber": 9912298,
            "contractStart": "2016-08-23T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "pointOfContact": "Ramsey Abbott",
            "phoneNumber" : "719-555-2636",
            "email": "Gould@email.com",
            "website": "www.Gamble.com",
            "status": "Inactive",
            "invoices": [
                {
                    "invoiceId": 2,
                    "invoiceNumber": "17POS/092790A",
                    "accountNumber": "5221000",
                    "expense": 20,
                    "vendor": {
                        "vendorId": 1,
                        "name": "Grainger"
                    },
                    "invoiceType": "Equipment",
                    "invoiceDate": "2017-08-23T12:30:00",
                    "description": "Donec vestibulum convallis tortor"
                },
                {
                    "invoiceId": 2,
                    "invoiceNumber": "17POS/092790A",
                    "accountNumber": "5221000-3-4",
                    "expense": 45.23,
                    "vendor": {
                        "vendorId": 1,
                        "name": "Grainger"
                    },
                    "invoiceType": "Equipment",
                    "invoiceDate": "2017-08-23T12:30:00",
                    "description": "unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, "
                },
                {
                    "invoiceId": 3,
                    "invoiceNumber": "10007558012",
                    "accountNumber": "5221000",
                    "expense": 578,
                    "vendor": {
                        "vendorId": 1,
                        "name": "Grainger"
                    },
                    "invoiceType": "Equipment",
                    "invoiceDate": "2017-08-23T12:30:00",
                    "description": "Etiam tincidunt cursus ipsum, vitae consequa"
                },
                {
                    "invoiceId": 3,
                    "invoiceNumber": "10007558012",
                    "accountNumber": "5221000-3-4",
                    "expense": 545.23,
                    "vendor": {
                        "vendorId": 1,
                        "name": "Grainger"
                    },
                    "invoiceType": "Equipment",
                    "invoiceDate": "2017-08-23T12:30:00",
                    "description": "First"
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
                        "name": "Big ole 2x4 Extra Woody",
                        "amount": 1,
                        "unit": "Gallon"
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
                        "name": "Big ole 2x4 Extra Woody",
                        "amount": 2.6,
                        "unit": "Ton"
                    },
                    "cost": 12.00
                }
            ],
            "materials": [
                {
                    "name": "Cold mix for winter patching",
                    "unit": "Ton",
                    "cost": 20.23
                },
                {
                    "name": "Plant mixed asphaltic surfacing material grading 3/8 minus plant mix seal (PMS) PG 64-22",
                    "unit": "Pints",
                    "cost": 3.50
                }
            ]
        }
      
}
